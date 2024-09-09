namespace AssetsManagementSystem.DTOs.DocumentDTOs
{
    public class UpdateDocumentRequestDTO
    {
        [Required(ErrorMessage = "Document title is required.")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        [MinLength(3, ErrorMessage = "Title cannot be less than 3 characters.")]
        public string Title { get; set; }


        [Required(ErrorMessage = "PDF file is required.")]
        public IFormFile PdfFile { get; set; }
    }
}
