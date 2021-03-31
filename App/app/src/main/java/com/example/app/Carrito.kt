package com.example.app

import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.*
import androidx.appcompat.app.AppCompatActivity
import kotlinx.android.synthetic.main.compras.*

class Carrito: AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.compras)

        var cantidad_producto = 0
        var preparacion = 0
        val total = findViewById<TextView>(R.id.lbltotal) as TextView
        val item = findViewById<TextView>(R.id.lblitem) as TextView
        val intent = getIntent()
        val platillos_recibidos = intent.getStringArrayListExtra("platillos")
        val precio_recibidos = intent.getStringArrayListExtra("precios")
        val codigo_recibidos = intent.getStringArrayListExtra("codigos")
        val tiempo_recibidos = intent.getStringArrayListExtra("tiempos")
        var costoinidividual = 0
        var tiempo_pre = 0
        var indice = 0
        val menu = findViewById<Spinner>(R.id.spnseleccinados)
        val menu_lista = ArrayAdapter(this,android.R.layout.simple_spinner_item, platillos_recibidos)

        menu.adapter = menu_lista
        menu.onItemSelectedListener = object: AdapterView.OnItemSelectedListener{

            override fun onItemSelected(parent: AdapterView<*>?, view: View?, position: Int, id: Long) {

                val seleccion = platillos_recibidos[position].toString()
                val seleccion2 = precio_recibidos[position].toString()

                item.setText(seleccion)
                costoinidividual = precio_recibidos[position].toString().toInt()
                tiempo_pre = tiempo_recibidos[position].toString().toInt()
                indice = position
            }
            override fun onNothingSelected(parent: AdapterView<*>?) {
                TODO("Not yet implemented")
            }
        }

        btnmas.setOnClickListener {
            cantidad_producto += costoinidividual
            preparacion += tiempo_pre
            total.setText(cantidad_producto.toString())
        }

        btnmenos.setOnClickListener {
            cantidad_producto -= costoinidividual
            preparacion -= tiempo_pre
            total.setText(cantidad_producto.toString())
        }

        btneliminar.setOnClickListener {
            platillos_recibidos.removeAt(indice)
            precio_recibidos.removeAt(indice)
        }

        btnconfirmar.setOnClickListener {

            if (lbltotal.text.toString() == "total"){
                Toast.makeText(this, "Favor agregar un platillo al carrito", Toast.LENGTH_LONG).show()
            }
            else
            {
            val intent = Intent(this, Pedidos::class.java)
            intent.putExtra("total", cantidad_producto.toString())
            intent.putExtra("orden", platillos_recibidos)
            intent.putExtra("tiempo", preparacion.toString())
            startActivity(intent)
            }
        }
    }
}