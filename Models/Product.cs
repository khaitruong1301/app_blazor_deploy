 
 namespace app_authen;
 public partial class ProductModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool Deleted { get; set; } = true;
    }