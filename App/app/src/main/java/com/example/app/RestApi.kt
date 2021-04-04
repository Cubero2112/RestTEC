import com.example.app.Seleccion
import com.example.app.Users
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.Headers
import retrofit2.http.POST

interface RestApi {
        @Headers("Content-Type: application/json")
        @POST("client")
        fun addUser(@Body userData: Users): Call<Users>
        fun vefiqueUser(@Body userData: Users): Call<Users>
        fun addPedido(@Body seleccionData: Seleccion): Call<Seleccion>
}