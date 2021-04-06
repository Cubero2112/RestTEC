package com.example.app

import RestApiService
import android.content.Intent
import android.os.Bundle
import android.widget.*
import androidx.appcompat.app.AppCompatActivity
import kotlinx.android.synthetic.main.singin.*
import timber.log.Timber

class SingIn: AppCompatActivity() {

    var usuarios_registrados_r = ArrayList<String>()
    var contrsenas_registradas_r = ArrayList<String>()

    override fun onCreate(savedInstanceState: Bundle?) {

        super.onCreate(savedInstanceState)
        setContentView(R.layout.singin)

        //inputdia.setOnClickListener{ SelecciondeFecha() }

        val usuario_input2 = findViewById<EditText>(R.id.inputcorreo) as EditText
        val contrasena_input2 =  findViewById<EditText>(R.id.inputcontrasenar) as EditText
        val name_input = findViewById<EditText>(R.id.inputuser) as EditText
        val Fname_input = findViewById<EditText>(R.id.inputpPnombre) as EditText
        val Lname_input = findViewById<EditText>(R.id.inputsapellido) as EditText
        val id_input = findViewById<EditText>(R.id.inputcedula) as EditText
        //val date_input = findViewById<EditText>(R.id.inputfechac) as EditText
        val dia_input = findViewById<TextView>(R.id.inputdia) as EditText
        val mes_input = findViewById<TextView>(R.id.inputmes) as EditText
        val anio_input = findViewById<TextView>(R.id.inputanio) as EditText
        val provincia_input = findViewById<TextView>(R.id.inputprovincia) as EditText
        val canton_input = findViewById<TextView>(R.id.inputcanton) as EditText
        val distrito_input = findViewById<TextView>(R.id.inputdistrito) as EditText
        val phone_input = findViewById<EditText>(R.id.inputtelefono) as EditText

        btnregistrar.setOnClickListener {

            val usuario = usuario_input2.text.toString()
            val contrasena = contrasena_input2.text.toString()
            val nombre = name_input.text.toString()
            val p_nombre = Fname_input.text.toString()
            val apellido = Lname_input.text.toString()
            val cedula = id_input.text.toString()
            //val fecha = date_input.text.toString()
            val dia_nacido = dia_input.text.toString()
            val mes_nacido = mes_input.text.toString()
            val anio_nacido = anio_input.text.toString()
            val provincia = provincia_input.text.toString()
            val canton = canton_input.text.toString()
            val distrito = distrito_input.text.toString()
            val telefono = phone_input.text.toString()
            val registro_usuario = Almacenamiento(usuario, usuarios_registrados_r)
            val registro_contrasena = Almacenamiento(contrasena, contrsenas_registradas_r)
            val intent = Intent(this, Main::class.java)

            intent.putExtra("usuario", registro_usuario)
            startActivity(intent)

            val apiService = RestApiService()
            val userInfo = Users(
                    userName = nombre,
                    userPassword = contrasena,
                    userEmail = usuario,
                    userID = cedula,
                    userFName = p_nombre,
                    userLName = apellido,
                //userDate = fecha,
                    userDay = dia_nacido,
                    userMonth = mes_nacido,
                    userAnio = anio_nacido,
                    userProvincia = provincia,
                    userCanton = canton,
                    userDistrito = distrito,
                    userPhone = telefono
                )

            apiService.addUser(userInfo) {
                if (it?.userName != null) {
                } else {
                    Timber.d("Error registering new user")
                }
            }

            this.finish()
        }
    }



    fun Almacenamiento(elemento: String, elementos: ArrayList<String>): ArrayList<String>{

        if (elementos.size == 0){
            elementos.add(elemento)
            return elementos
        }
        else{
            val posicion = elementos.size -1
            elementos.add(posicion, elemento)
            return elementos
        }
    }
}
