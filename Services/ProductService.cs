using app_authen;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

public class ProductStoreService
{
    private readonly HttpClient _httpStore;
    public ProductStoreService(IServiceProvider serviceProvider)
    {
        _httpStore = serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient("apisv"); ;
    }
    public async Task<ProductModel[]> GetAllProductAsync(string keyword = "")
    {
        // Sử dụng ExternalHttpClient
        string url = "/api/Product";
        if (!string.IsNullOrEmpty(keyword))
        {
            url += $"{url}?keyword={keyword}";
        }
        var res = await _httpStore.GetFromJsonAsync<ProductModel[]>(url);
        return res;
    }


    public async Task<string> CreateProduct(ProductModel newProduct)
    {
        // Gửi yêu cầu POST với dữ liệu dưới dạng JSON
        var response = await _httpStore.PostAsJsonAsync("/api/ProductApi/create", newProduct);
        // Kiểm tra nếu phản hồi trả về thành công (status code 200-299)
        if (response.IsSuccessStatusCode)
        {
            // Đọc nội dung từ phản hồi và trả về
            var responseContent = await response.Content.ReadFromJsonAsync<string>();
            return responseContent; // Trả về nội dung của phản hồi
        }
        else
        {
            // Xử lý nếu có lỗi trong phản hồi
            throw new Exception($"Error creating product: {response.StatusCode}");
        }
    }




    // public async Task<ProductStore> GetProductById(string id = "")
    // {
    //     if (!string.IsNullOrEmpty(id))
    //     {
    //         // Sử dụng ExternalHttpClient
    //         var res = await _httpStore.GetFromJsonAsync<HttpResponse<ProductStore>>($"api/Product/getid?id={id}");
    //         var productModel = FunctionUtility.MapToModel<ProductStore>(res.content);
    //         return productModel;
    //     }
    //     return new ProductStore();
    //     // var apiResponse = new ApiResponse { Id = 1, Name = "Product A", Price = 100, Alias = "PA", Size = "M" };
    //     // var productModel = MapToModel<ProductModel>(apiResponse);

    // }
    // public async Task<ProductStore[]> UpdateProduct(int id, ProductStore product)
    // {
    //     // Sử dụng ExternalHttpClient
    //     var res = await _httpStore.GetFromJsonAsync<HttpResponse<ProductStore[]>>("api/Product");
    //     return res.content;
    // }
    // public async Task<ProductStore[]> DeleteProduct(int id)
    // {
    //     // Sử dụng ExternalHttpClient
    //     var res = await _httpStore.GetFromJsonAsync<HttpResponse<ProductStore[]>>("api/Product");
    //     return res.content;
    // }
}
