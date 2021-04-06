package com.example.app

import RestApiService
import android.content.Intent
import android.os.Bundle
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import kotlinx.android.synthetic.main.content_main.*
import timber.log.Timber

class Main : AppCompatActivity() {

    var usuarios_registrados = ArrayList<String>()
    var contrsenas_registradas = ArrayList<String>()

    override fun onCreate(savedInstanceState: Bundle?) {

        super.onCreate(savedInstanceState)
        setContentView(R.layout.main)

        val usuario_input = findViewById<EditText>(R.id.inputusuario) as EditText
        val contrasena_input = findViewById<EditText>(R.id.inputcontrasena) as EditText
        val label = findViewById<TextView>(R.id.lblnombreu) as TextView

        // Usuario: estudiante@estudiantes.ac.cr
        // Contraseñas_ 123456
        usuarios_registrados.add("estudiante@estudiantes.ac.cr")
        contrsenas_registradas.add("123456")

        btnentrar.setOnClickListener {

            val usuario = usuario_input.text.toString()
            val contrasena = contrasena_input.text.toString()
            //val registro_usuario = Almacenamiento(usuario, usuarios_registrados)
            //val registro_contrasena = Almacenamiento(contrasena, contrsenas_registradas)




            if (usuario_input.text.toString().isNullOrEmpty() || contrasena_input.text.toString().isNullOrEmpty() ){

                Toast.makeText(this, "Favor ingresar datos válidos", Toast.LENGTH_LONG).show()
            }
            else{
                startActivity(Intent(this, Comidas::class.java))
            }

            val apiService = RestApiService()
            val userInfo = Users(userName = "",
                    userFName = "",
                    userLName = "",
                    userID = "",
                    //userDate = "",
                    userPhone = "",
                    userDay = "",
                    userMonth = "",
                    userAnio = "",
                    userProvincia = "",
                    userCanton = "",
                    userDistrito = "",
                    userPassword = contrasena,
                    userEmail = usuario)


            //intent.putExtra("usuario", registro_usuario)
           // startActivity(intent)

            apiService.addUser(userInfo) {
                if (it?.userName != null) {
                } else {
                    Timber.d("Error user not register")
                }
            }

            this.finish()
        }

        /*fun Almacenamiento(elemento: String, elementos: ArrayList<String>): ArrayList<String>{

            if (elementos.size == 0){
                elementos.add(elemento)
                return elementos
            }
            else{
                val posicion = elementos.size -1
                elementos.add(posicion, elemento)
                return elementos
            }
        }*/

        btnregistrarse.setOnClickListener {
            startActivity(Intent(this, SingIn::class.java))
        }
    }
}
