package com.example.app

import RestApiService
import android.content.Intent
import android.os.Bundle
import android.widget.*
import androidx.appcompat.app.AppCompatActivity
import kotlinx.android.synthetic.main.login.*
import timber.log.Timber

class LogIn: AppCompatActivity() {

    var usuarios_registrados_r = ArrayList<String>()
    var contrsenas_registradas_r = ArrayList<String>()

    override fun onCreate(savedInstanceState: Bundle?) {

        super.onCreate(savedInstanceState)
        setContentView(R.layout.login)

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
            val userInfo = Users(  userName = "",
                userFName = p_apellido,
                userLName = s_apellido,
                userID = cedula,
                userDate = fecha,
                userPhone = telefono,
                userPassword = contrasena,
                userEmail = usuario)

            apiService.addUser(userInfo) {
                if (it?.userName != null) {
                    // it = newly added user parsed as response
                    // it?.id = newly added user ID
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

























/*

//Se declaran las variables para la sección de los spinner de provincia, canton y  dsitrito
        val provincia = findViewById<Spinner>(R.id.spnprovincia)
        val canton = findViewById<Spinner>(R.id.spncanton)
        val distrito = findViewById<Spinner>(R.id.spndistrito)

        //Se toman las listas de arrays creadas en la sección de values/strings del proyecto para
        //poder trabajar con ellos y mostrar lo que estan almacenan en la interfaz
        val provincias_lista = resources.getStringArray(R.array.provincias)

        val opciones_provincias = ArrayAdapter(this,android.R.layout.simple_spinner_dropdown_item, provincias_lista)
        provincia.adapter = opciones_provincias

        //Se implementa la función propia del spinner para cuando sea seleccionado un elemento del mismo
        provincia.onItemSelectedListener = object: AdapterView.OnItemSelectedListener{
            //Función implementada cuando es seleccionado uno de los elementos del spinner de provincias
            //El elemento importante es el id, ya que este dará la posición en el array de opciones
            override fun onItemSelected(parent: AdapterView<*>?, view: View?, position: Int, id: Long) {

                //Se selecciona la primera letra de la opción de provincia, ya que para esto
                //Se tiene que llamar de inmediato el spinner de los cantones según dicha provincia
                //El array de cantones tiene por nombre Iniicial de Provincia en Mayúscula + cantones
                val seleccion = provincias_lista[position].first().toString() + "cantones"
                Toast.makeText(this@Registrarse, seleccion,Toast.LENGTH_LONG).show()

            }
            override fun onNothingSelected(parent: AdapterView<*>?) {
                TODO("Not yet implemented")
            }
        }
* */