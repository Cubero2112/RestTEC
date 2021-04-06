package com.example.app

import android.content.Intent
import android.os.Bundle
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import kotlinx.android.synthetic.main.pedido.*

class Pedidos: AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.pedido)

        val label = findViewById<TextView>(R.id.lbltotala) as TextView
        val orden_display = findViewById<TextView>(R.id.lblplatillos) as TextView
        val intent = getIntent()
        val total = intent.getStringExtra("total")
        val orden = intent.getStringArrayListExtra("orden")
        val tiempo = intent.getStringExtra("tiempo")

        label.setText("Total: $total")
        orden_display.setText(orden.toString())

        val calendario:java.util.Calendar = java.util.Calendar.getInstance()
        val dia = calendario.get(java.util.Calendar.DAY_OF_MONTH)
        val mes = calendario.get(java.util.Calendar.MONTH)
        val ano = calendario.get(java.util.Calendar.YEAR)
        val hora = calendario.get(java.util.Calendar.HOUR_OF_DAY)
        val minuto = calendario.get(java.util.Calendar.MINUTE)
        val fecha = findViewById<TextView>(R.id.lblhora) as TextView

        fecha.text = "$dia-$mes-$ano   $hora-$minuto"

        val consecutivo = findViewById<TextView>(R.id.lblconsec)

        consecutivo.setText("0001")

        btnvisualizar.setOnClickListener {
            val intent = Intent(this, Visualizar::class.java)
            intent.putExtra("preparacion", tiempo)
            startActivity(intent)
        }
    }
}