package com.example.app

import RestApiService
import android.content.Intent
import android.os.Bundle
import android.os.Handler
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import kotlinx.android.synthetic.main.feedback.*
import kotlinx.android.synthetic.main.visualizar.*
import timber.log.Timber
import java.security.KeyStore

class Feedback : AppCompatActivity(){
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.feedback)

        val calendario:java.util.Calendar = java.util.Calendar.getInstance()
        val dia = calendario.get(java.util.Calendar.DAY_OF_MONTH)
        val mes = calendario.get(java.util.Calendar.MONTH)
        val ano = calendario.get(java.util.Calendar.YEAR)
        val hora = calendario.get(java.util.Calendar.HOUR_OF_DAY)
        val minuto = calendario.get(java.util.Calendar.MINUTE)
        val fechahora = findViewById<TextView>(R.id.lblfecha_hora) as TextView
        val comentario = findViewById<EditText>(R.id.inputfeedback) as EditText

        fechahora.text = "$dia-$mes-$ano   $hora-$minuto"

        var puntaje:Float = 0.0f

        ratingBar.setOnRatingBarChangeListener{ratingBar, calificacion, b->
            Toast.makeText(this, calificacion.toString(), Toast.LENGTH_SHORT).show()
            puntaje = calificacion
            }

        btnenviar.setOnClickListener {

            /*val comida_selec1 = usuario_input2.text.toString()
            val comida_selec2 = contrasena_input2.text.toString()
            val comida_selec3 = name_input.text.toString()
            val fecha = Fname_input.text.toString()
            val telefono = phone_input.text.toString()
            val correo =
            val intent = Intent(this, Main::class.java)

            intent.putExtra("usuario", registro_usuario)
            startActivity(intent)

            val apiService = RestApiService()
            val userInfo = Seleccion(
                    selecComida1 = nombre,
                    selecComida2 = p_apellido,
                    selecComida3 = s_apellido,
                    selecDate = fecha,
                    selecPhone = telefono,
                    userEmail = correo)

            apiService.addUser(userInfo) {
                if (it?.userName != null) {
                } else {
                    Timber.d("Error registering new user")
                }
            }*/

            this.finish()
        }


    }
}

