package com.example.app

import android.content.Intent
import android.os.Bundle
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import kotlinx.android.synthetic.main.seleccion.*

class Comidas: AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.seleccion)

        val platillo: ArrayList<Platos> = arrayListOf(
            Platos(0,"Ensalada", 100,"Hola soy una ensalada", 200,"comida", 3,9,3, "Bueno") ,
            Platos(1,"Frijoles", 400,"Frijolitos", 300,"comida", 5,4,8, "Bueno"),
            Platos(2,"Arroz", 222,"Arrozzzz", 600,"comida", 4,7,8, "Bueno"),
            Platos(3,"Lasagna", 333,"Una Lasagna", 800,"comida", 4,6,0, "Bueno"),
            Platos(4,"Macarrones", 444,"Unos macarrones", 420,"comida", 4,0,0, "Bueno"),
            Platos(5,"Pollo", 600,"Un Pollo", 380,"carne", 4,8,7, "Bueno"),
            Platos(6,"Pinto", 444,"Un pinto", 680,"comida", 4,7,6, "Bueno"),
            Platos(7,"Huevo", 666,"Un huevo", 450,"comida", 4,3,7, "Bueno"),
            Platos(8,"Chorizo", 677,"Un chorizo", 500,"comida", 3,2,6, "Bueno"),
            Platos(9,"Frutas", 445,"Unas frutas", 300,"comida", 4,4,2, "Bueno")
        )

        val total_menu = platillo.size
        var numero_platillo = 0
        val platillos_seleccionados = ArrayList<String>()
        val precio_seleccionados = ArrayList<String>()
        val codigo_seleccionados = ArrayList<String>()
        val tiempo_seleccionados = ArrayList<String>()
        val fondo = findViewById<TextView>(R.id.lblfondo) as TextView
        val nombre_menu = findViewById<TextView>(R.id.lblmenu) as TextView
        val descripcion_menu = findViewById<TextView>(R.id.lbldes) as TextView
        val precio_menu = findViewById<TextView>(R.id.lblprecio) as TextView

        nombre_menu.setText(platillo.get(numero_platillo).nombre)
        descripcion_menu.setText(platillo.get(numero_platillo).descripcion)
        precio_menu.setText(platillo.get(numero_platillo).precio.toString())

        btnatras.setOnClickListener {
            if (numero_platillo <= 0){
                numero_platillo = total_menu
            }
            else{
                numero_platillo--
                nombre_menu.setText(platillo.get(numero_platillo).nombre)
                descripcion_menu.setText(platillo.get(numero_platillo).descripcion)
                precio_menu.setText(platillo.get(numero_platillo).precio.toString())
            }
        }

        btnsiguiente.setOnClickListener {
            if (numero_platillo >= total_menu){
                numero_platillo = 0
            }
            else{
                numero_platillo++
                nombre_menu.setText(platillo.get(numero_platillo).nombre)
                descripcion_menu.setText(platillo.get(numero_platillo).descripcion)
                precio_menu.setText(platillo.get(numero_platillo).precio.toString())
            }
        }

        btnagregar.setOnClickListener {
                codigo_seleccionados.add(platillo.get(numero_platillo).codigo.toString())
                platillos_seleccionados.add(platillo.get(numero_platillo).nombre)
                precio_seleccionados.add(platillo.get(numero_platillo).precio.toString())
                tiempo_seleccionados.add(platillo.get(numero_platillo).tiempo.toString())
                Toast.makeText(this, platillos_seleccionados.toString(), Toast.LENGTH_LONG).show()
        }

        btncomprar.setOnClickListener {
            if(platillos_seleccionados.size == 0){
                Toast.makeText(this, "Debe agregar un item al carrito", Toast.LENGTH_LONG).show()
            }
            else {
                val intent = Intent(this, Carrito::class.java)
                intent.putExtra("platillos", platillos_seleccionados)
                intent.putExtra("precios", precio_seleccionados)
                intent.putExtra("codigos", codigo_seleccionados)
                intent.putExtra("tiempos", tiempo_seleccionados)
                startActivity(intent)
            }
        }
    }
}