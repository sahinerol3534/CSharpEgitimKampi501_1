<!DOCTYPE html>
<html lang="tr">
<head>
<body>
    <h1>Murat YÜCEDAĞ ile C# Eğitim Kampı.</h1>
    <p style="font-family: Arial; font-size: 12pt;">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'Benim gibi Yazılıma yeni başlayanlar için iyi bir kaynak olması dilğiyle'
    </p><br><br>

  <div style="display: flex; align-items: center;">
        <img src="23.jpeg" alt="" width="80px">
        <span style="font-size: 18pt; margin-left: 10px;"><strong>23. Ders Dapper kullanmadan dosya yapısıyla yazılan kod<br>
        Bu kod yazımında <img src="th.png" alt="" width="18px">CharGPT etkin olarak kullanılmıştır.</strong></span><br>
  </div> 
 
     

   <p style="font-family: Arial; font-size: 12pt;"> Uygulamayı bititdikten sonra  <img src="th.png" alt="" width="18px">CharGPT'den anlatmasını istedim;<br>
     Bu uygulamanın kodlarını adım adım anlatır mısın<br>
<img src="th.png" alt="" width="18px">CharGPT:<br>

Tabii, bu uygulamanın kodlarını adım adım inceleyelim:<br>

1. Program.cs - Uygulamanın Başlatılması<br>
<br></br>
using System;<br>
using System.Collections.Generic;<br>
using System.Linq;<br>
using System.Threading.Tasks;<br>
using System.Windows.Forms;<br><br>

namespace CSharpEgitimKampi501_1<br>
{<br>
&nbsp;&nbsp;&nbsp;&nbsp;internal static class Program<br>
&nbsp;&nbsp;&nbsp;&nbsp;{<br>
         <summary><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;/// The main entry point for the application.<br>
         </summary><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[STAThread]<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;static void Main()<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    Application.EnableVisualStyles();<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    Application.SetCompatibleTextRenderingDefault(false);<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    Application.Run(new Form1());<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<br>
&nbsp;&nbsp;&nbsp;&nbsp;}<br>
}<br>
Amacı: Uygulamanın başlangıç noktasıdır. Bu sınıf, Windows Forms uygulamasını başlatır ve Form1 formunu çalıştırır.<br>
Application.EnableVisualStyles(): Uygulamanın görsel stillerini aktifleştirir.<br>
Application.SetCompatibleTextRenderingDefault(false): Metin render’ı için varsayılanı ayarlamak için kullanılır.<br>
Application.Run(new Form1()): Form1 sınıfını başlatır ve kullanıcı arayüzünü çalıştırır.<br>
2. ProductRepository - Veritabanı İşlemleri<br>
ProductRepository sınıfı, ürünlerle ilgili veritabanı işlemlerini gerçekleştiren sınıftır. Bu sınıf, IProductRepository arayüzünü uygular.<br>
<br>
Bağlantı Dizesi<br>
<br>
private readonly string _connectionString = "server = SAHINEROL; initial catalog = EgitimKampi501Db; integrated security = true";<br>
Amacı: Veritabanına bağlanmak için kullanılan bağlantı dizesini içerir. Burada SQL Server'a bağlanmak için kullanılan bilgiler yer alır.<br>
Ürün Ekleme<br>
<br>
public async Task CreateProductAsync(CreateProductDto createProductDto)<br>
{<br>
&nbsp;&nbsp;&nbsp;&nbsp;using (SqlConnection connection = new SqlConnection(_connectionString))<br>
&nbsp;&nbsp;&nbsp;&nbsp;{<br>
&nbsp;&nbsp;&nbsp;&nbsp;string query = "INSERT INTO TblProduct(ProductName, ProductStock, ProductPrice, ProductCatagory)" +<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"VALUES(@productName, @productStock, @productPrice, @productCatagory)";<br>
&nbsp;&nbsp;&nbsp;&nbsp;<br>
&nbsp;&nbsp;&nbsp;&nbsp;SqlCommand command = new SqlCommand(query, connection);<br>
&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.AddWithValue("@productName", createProductDto.ProductName);<br>
&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.AddWithValue("@productStock", createProductDto.ProductStock);<br>
&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.AddWithValue("@productPrice", createProductDto.ProductPrice);<br>
&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.AddWithValue("@productCatagory", createProductDto.ProductCatagory);<br>
&nbsp;&nbsp;&nbsp;&nbsp;<br>
&nbsp;&nbsp;&nbsp;&nbsp;connection.Open();<br>
&nbsp;&nbsp;&nbsp;&nbsp;await command.ExecuteNonQueryAsync();<br>
&nbsp;&nbsp;&nbsp;&nbsp;}<br>
}<br>
Amacı: Bu metod, yeni bir ürün ekler. CreateProductDto sınıfından alınan verilerle SQL sorgusu oluşturulup çalıştırılır.<br>
SqlConnection ile veritabanına bağlanılır.<br>
SqlCommand ile veritabanı sorgusu (INSERT INTO TblProduct) çalıştırılır.<br>
Parametreler, SQL enjeksiyonuna karşı korunmuş şekilde AddWithValue ile sorguya eklenir.<br>
Ürün Silme<br>
<br>
public async Task DeleteProductAsync(int id)<br>
{<br>
&nbsp;&nbsp;&nbsp;&nbsp;using (SqlConnection connection = new SqlConnection(_connectionString))<br>
&nbsp;&nbsp;&nbsp;&nbsp;{<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;string query = "DELETE FROM TblProduct WHERE ProductId = @productId";<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlCommand command = new SqlCommand(query, connection);<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.AddWithValue("@productId", id);<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;connection.Open();<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;await command.ExecuteNonQueryAsync();<br>
&nbsp;&nbsp;&nbsp;&nbsp;}<br>
}<br>
Amacı: Belirtilen ID'ye sahip ürünü veritabanından siler.<br>
DELETE FROM TblProduct WHERE ProductId = @productId sorgusu çalıştırılır.<br>
Tüm Ürünleri Getirme<br>
<br>
public async Task<List<ResultProductDto>> GetAllProductAsync()<br>
{<br>
&nbsp;&nbsp;&nbsp;&nbsp;var products = new List<ResultProductDto>();<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp;using (SqlConnection connection = new SqlConnection(_connectionString))<br>
&nbsp;&nbsp;&nbsp;&nbsp;{<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;string query = "SELECT * FROM TblProduct";<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlCommand command = new SqlCommand(query, connection);<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;connection.Open();<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;using (SqlDataReader reader = await command.ExecuteReaderAsync())<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;while (await reader.ReadAsync())<br> 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;products.Add(new ResultProductDto<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ProductId = reader.GetInt32(0),<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ProductName = reader.GetString(1),<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ProductStock = reader.GetInt32(2),<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ProductPrice = reader.GetDecimal(3),<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ProductCatagory = reader.GetString(4),<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;});  <br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<br>
&nbsp;&nbsp;&nbsp;&nbsp;}<br>
&nbsp;&nbsp;&nbsp;&nbsp;return products;<br>
}<br>
Amacı: Veritabanındaki tüm ürünleri çeker ve bir liste halinde döndürür. Her bir ürün için ResultProductDto nesnesi oluşturulur.<br>
Ürün ID'sine Göre Getirme<br>
<br>
public async Task<ResultProductDto> GetByProductIdAsync(int id)<br>
{<br>
&nbsp;&nbsp;&nbsp;&nbsp;ResultProductDto product = null;<br>
&nbsp;&nbsp;&nbsp;&nbsp;using (SqlConnection connection = new SqlConnection(_connectionString))<br>
&nbsp;&nbsp;&nbsp;&nbsp;{<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;string query = "SELECT * FROM TblProduct WHERE ProductId = @productId";<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlCommand command = new SqlCommand(query, connection);<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.AddWithValue("@productId", id);<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;connection.Open();<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;using (SqlDataReader reader = await command.ExecuteReaderAsync()) <br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;if (await reader.ReadAsync())<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;product = new ResultProductDto<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ProductId = reader.GetInt32(0),<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ProductName = reader.GetString(1),<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ProductStock = reader.GetInt32(2),<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ProductPrice = reader.GetDecimal(3),<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ProductCatagory = reader.GetString(4)<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;};<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<br>
&nbsp;&nbsp;&nbsp;&nbsp;}<br>
&nbsp;&nbsp;&nbsp;&nbsp;return product;<br>
}<br>
Amacı: Verilen id parametresine göre belirli bir ürünü veritabanından çeker.<br>
Ürün Güncelleme<br>
<br>
public async Task UpdateProductAsync(UpdateProductDto updateProductDto)<br>
{<br>
&nbsp;&nbsp;&nbsp;&nbsp;using (SqlConnection connection = new SqlConnection(_connectionString))<br>
&nbsp;&nbsp;&nbsp;&nbsp;{<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;string query = "UPDATE TblProduct SET ProductName = @productName, ProductStock = @productStock," +<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;               "ProductPrice = @productPrice, ProductCatagory = @productCatagory WHERE ProductId = @productId";<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlCommand command = new SqlCommand(query, connection);<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.AddWithValue("@productName", updateProductDto.ProductName);<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.AddWithValue("@productStock", updateProductDto.ProductStock);<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.AddWithValue("@productPrice", updateProductDto.ProductPrice);<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.AddWithValue("@productCatagory", updateProductDto.ProductCatagory);<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Parameters.AddWithValue("@productId", updateProductDto.ProductId);<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;        <br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;connection.Open();<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;await command.ExecuteNonQueryAsync();<br>
&nbsp;&nbsp;&nbsp;&nbsp;}<br>
}<br>
Amacı: Veritabanındaki var olan bir ürünü günceller. Güncellenen ürün bilgileri UpdateProductDto sınıfından alınır.<br>
3. DTO (Data Transfer Object) Sınıfları<br>
CreateProductDto: Yeni ürün eklerken kullanılacak verileri taşır.<br>
UpdateProductDto: Var olan ürünü güncellerken kullanılan verileri taşır.<br>
ResultProductDto: Veritabanından gelen ürün verilerini taşır.<br>
4. IProductRepository Arayüzü<br>
Bu arayüz, ProductRepository sınıfının metodlarını tanımlar. Bu metodlar:<br>
<br>
Ürün ekleme,<br>
Ürün silme,<br>
Ürün güncelleme,<br>
Ürünleri çekme gibi işlemleri içerir.<br>
5. Form1 - UI (Kullanıcı Arayüzü)<br>
Form1, kullanıcıya ürünleri görüntüleme, ekleme, silme ve güncelleme işlemleri için bir arayüz sunar. Buradaki formda kullanıcı, uygulama ile etkileşimde bulunur ve gerekli işlemler yapılır.<br>
<br>
Özet<br>
Bu uygulama, veritabanı üzerinden ürünlerle ilgili CRUD (Create, Read, Update, Delete) işlemleri gerçekleştiren bir yapı sunar. Kullanıcıların veritabanıyla etkileşimde<br> bulunabilmesi için, Windows Forms UI ve veritabanı erişim katmanı kullanılmıştır. ProductRepository sınıfı, veritabanı işlemlerini yönetirken, <br>IProductRepository arayüzü bu işlemleri daha esnek ve test edilebilir hale getirir. <br>     
    </p>
    --------------------------------------------<br><br>
    







</body>
</html>
