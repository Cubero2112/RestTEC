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

        inputfechac.setOnClickListener{ SelecciondeFecha() }

        val usuario_input2 = findViewById<EditText>(R.id.inputcorreor) as EditText
        val contrasena_input2 =  findViewById<EditText>(R.id.inputcontrasenar) as EditText
        val name_input = findViewById<EditText>(R.id.inputnombre) as EditText
        val Fname_input = findViewById<EditText>(R.id.inputpapellido) as EditText
        val Lname_input = findViewById<EditText>(R.id.inputsapellido) as EditText
        val id_input = findViewById<EditText>(R.id.inputcedula) as EditText
        val date_input = findViewById<EditText>(R.id.inputfechac) as EditText
        val phone_input = findViewById<EditText>(R.id.inputtelefono) as EditText

        btnregistrar.setOnClickListener {

            val usuario = usuario_input2.text.toString()
            val contrasena = contrasena_input2.text.toString()
            val nombre = name_input.text.toString()
            val p_apellido = Fname_input.text.toString()
            val s_apellido = Lname_input.text.toString()
            val cedula = id_input.text.toString()
            val fecha = date_input.text.toString()
            val telefono = phone_input.text.toString()
            val registro_usuario = Almacenamiento(usuario, usuarios_registrados_r)
            val registro_contrasena = Almacenamiento(contrasena, contrsenas_registradas_r)
            val intent = Intent(this, Main::class.java)

            intent.putExtra("usuario", registro_usuario)
            startActivity(intent)

            val apiService = RestApiService()
            val userInfo = Users(
                    userName = nombre,
                userFName = p_apellido,
                userLName = s_apellido,
                userID = cedula,
                userDate = fecha,
                userPhone = telefono,
                userPassword = contrasena,
                userEmail = usuario)

            apiService.addUser(userInfo) {
                if (it?.userName != null) {
                } else {
                    Timber.d("Error registering new user")
                }
            }

            this.finish()
        }
    }

    private fun SelecciondeFecha(){
       val datePicker = SeleccionFecha {dia, mes, ano -> Seleccion(dia, mes, ano)}
        datePicker.show(supportFragmentManager, "Fecha")
    }

    fun Seleccion(dia:Int, mes:Int, ano:Int){
        inputfechac.setText("$dia-$mes-$ano")
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
