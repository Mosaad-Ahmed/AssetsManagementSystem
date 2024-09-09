using AssetsManagementSystem.DTOs.DocumentDTOs;
using AssetsManagementSystem.Others.Storage;

namespace AssetsManagementSystem.Services.Document
{
    public class DocumentService : BaseClassForServices
    
    {
        private readonly IFileService _fileService;
 
        public DocumentService(IUnitOfWork unitOfWork, 
            Others.Interfaces.IAutoMapper.IMapper mapper,
            IHttpContextAccessor httpContextAccessor, IFileService fileService)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
            _fileService = fileService;
        }


        #region Add Document
        public async Task<GetDocumentResponseDTO> AddDocumentAsync(AddDocumentRequestDTO addDocumentDto)
        {
            Asset asset = await UnitOfWork.readRepository<Asset>().GetAsync(predicate: a => a.Id == addDocumentDto.AssetId &&
                                                                                a.Status != AssetStatus.Retired.ToString() &&
                                                                                (a.IsDeleted == false || a.IsDeleted == null));

            if (asset is null)
                throw new InvalidOperationException("this asset no longer exist");

            // Upload the PDF file
            var filePath = await _fileService.UploadPdfAsync(addDocumentDto.PdfFile.OpenReadStream(), addDocumentDto.PdfFile.FileName);
       
             var document = Mapper.Map<Models.DbSets.Document>(addDocumentDto);

            document.FilePath = filePath;

            document.Title = addDocumentDto.Title;

            document.AddedOnDate = DateTime.Now;

            document.UploadedById = Guid.Parse(UserId);

            document.AssetId = addDocumentDto.AssetId;
           
            await UnitOfWork.BeginTransactionAsync();
            try
            {
                await UnitOfWork.writeRepository<Models.DbSets.Document>().AddAsync(document);
                await UnitOfWork.SaveChangeAsync();
                await UnitOfWork.CommitTransactionAsync();

                return await GetDocumentByIdAsync(document.Id);
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                _fileService.RemovePdf(filePath);  
                throw;
            }
        }
        #endregion


        #region Update Document
        public async Task<GetDocumentResponseDTO> UpdateDocumentAsync(int id, UpdateDocumentRequestDTO updateDocumentDto)
        {
            var existingDocument = await UnitOfWork.readRepository<Models.DbSets.Document>().GetAsync(d => d.Id == id
                                &&(d.IsDeleted==false||d.IsDeleted==null));

            Asset asset = await UnitOfWork.readRepository<Asset>().GetAsync(predicate:a=>a.Id==existingDocument.AssetId&&
                                                                                a.Status!=AssetStatus.Retired.ToString()&&
                                                                                (a.IsDeleted==false||a.IsDeleted==null));

            if (asset is null)
                throw new InvalidOperationException("this asset no longer exist");
           

            if (existingDocument == null)
            {
                throw new KeyNotFoundException("Document not found.");
            }

            // Delete the old file
            _fileService.RemovePdf(existingDocument.FilePath);

            // Upload the new PDF file
            var filePath = await _fileService.UploadPdfAsync(updateDocumentDto.PdfFile.OpenReadStream(), updateDocumentDto.PdfFile.FileName);

            // Map updated fields to existing document
            existingDocument.Title = updateDocumentDto.Title;
            existingDocument.FilePath = filePath;
            existingDocument.UploadedById = Guid.Parse(UserId); ;
            existingDocument.AssetId = existingDocument.AssetId;
            existingDocument.UpdatedDate = DateTime.Now;
      

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                await UnitOfWork.writeRepository<Models.DbSets.Document>().UpdateAsync(id, existingDocument);
                await UnitOfWork.SaveChangeAsync();
                await UnitOfWork.CommitTransactionAsync();

                return await GetDocumentByIdAsync(id);
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                _fileService.RemovePdf(filePath); 
                throw;
            }
        }
        #endregion


        #region Get Document by ID
        public async Task<GetDocumentResponseDTO> GetDocumentByIdAsync(int id)
        {
            var document = await UnitOfWork.readRepository<Models.DbSets.Document>().GetAsync(d => d.Id == id);
            if (document == null)
            {
                throw new KeyNotFoundException("Document not found.");
            }

            var getDocumentResponseDTO = Mapper.Map<GetDocumentResponseDTO,Models.DbSets.Document>(document);

            getDocumentResponseDTO.UploadedByName=document.UploadedBy.FirstName+" "+document.UploadedBy.LastName;
            getDocumentResponseDTO.AssetName = document.Asset.Name;
             
            return getDocumentResponseDTO;

        }
        #endregion


        #region Remove Document
        public async Task RemoveDocumentAsync(int id)
        {
            var document = await UnitOfWork.readRepository<Models.DbSets.Document>().GetAsync(d => d.Id == id);
            if (document == null)
            {
                throw new KeyNotFoundException("Document not found.");
            }

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                _fileService.RemovePdf(document.FilePath);
                
                document.IsDeleted = true;
                document.DeletedDate = DateTime.Now;

                await UnitOfWork.writeRepository<Models.DbSets.Document>().UpdateAsync(document.Id,document);
                await UnitOfWork.SaveChangeAsync();
                await UnitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        #endregion


    }
}
