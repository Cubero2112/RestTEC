package com.example.app

import android.content.Intent
import android.os.Bundle
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import kotlinx.android.synthetic.main.content_main.*

class Main : AppCompatActivity() {

    var usuarios_registrados = ArrayList<String>()
    var contrsenas_registradas = ArrayList<String>()

    override fun onCreate(savedInstanceState: Bundle?) {

        super.onCreate(savedInstanceState)
        setContentView(R.layout.main)

        val usuario_input = findViewById<EditText>(R.id.inputusuario) as EditText
        val contrasena_inpurt = findViewById<EditText>(R.id.inputcontrasena) as EditText
        val label = findViewById<TextView>(R.id.lblnombreu) as TextView

        // Usuario: estudiantea@estudiantes.ac.cr
        // Contraseñas_ 123456
        usuarios_registrados.add("estudiantea@estudiantes.ac.cr")
        contrsenas_registradas.add("123456")

        btnentrar.setOnClickListener {

            val intent = getIntent()
            val usariorecibido = intent.getStringArrayListExtra("usuario")
            val cantrasenarecibo = intent.getStringExtra("contrasena")
            val usuario = usuario_input.text.toString()
            val contrasena = contrasena_inpurt.text.toString()

            if (usuario_input.text.toString().isNullOrEmpty() || contrasena_inpurt.text.toString().isNullOrEmpty() ){
                //Se despliega un mensaje de alerta solicitando datos válidos para el ingreso
                Toast.makeText(this, "Favor ingresar datos válidos", Toast.LENGTH_LONG).show()
            }
            else{
                startActivity(Intent(this, Comidas::class.java))
            }
        }

        btnregistrarse.setOnClickListener {
            startActivity(Intent(this, SingIn::class.java))
        }
    }
}
