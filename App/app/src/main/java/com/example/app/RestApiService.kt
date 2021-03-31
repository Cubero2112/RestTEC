import com.example.app.Users
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class RestApiService {
    fun addUser(userData: Users, onResult: (Users?) -> Unit){
        val retrofit = ServiceBuilder.buildService(RestApi::class.java)
        retrofit.addUser(userData).enqueue(
            object : Callback<Users> {
                override fun onFailure(call: Call<Users>, t: Throwable) {
                    onResult(null)
                }
                override fun onResponse(call: Call<Users>, response: Response<Users>) {
                    val addedUser = response.body()
                    onResult(addedUser)
                }
            }
        )
    }
}