package com.example.app

import android.animation.ObjectAnimator
import android.content.Intent
import android.os.Bundle
import android.os.Handler
import androidx.appcompat.app.AppCompatActivity
import kotlinx.android.synthetic.main.visualizar.*

class Visualizar: AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.visualizar)

        val cargar = Cargar(this)
        cargar.ComenzarCarga()
        val handler = Handler()

        handler.postDelayed(object :Runnable {
            override fun run() {
                cargar.isDissmis()
            }
        }, 5000)

        btnconcretar.setOnClickListener {
            startActivity(Intent(this, Feedback::class.java))
        }
    }
}
