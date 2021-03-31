package com.example.app

import android.content.Context
import android.content.Intent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.recyclerview.widget.RecyclerView
import kotlinx.android.synthetic.main.comida.view.*

class ComAdapter(val platillo:List<Platos>, private val contexto: Context):RecyclerView.Adapter<ComAdapter.AdaptadorPlatillos>(){

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): AdaptadorPlatillos {
        val layoutInflater = LayoutInflater.from(parent.context)
        return AdaptadorPlatillos(layoutInflater.inflate(R.layout.comida, parent, false), contexto)
    }

    override fun getItemCount(): Int {
        return platillo.size
    }

    override fun onBindViewHolder(holder: AdaptadorPlatillos, position: Int) {
        holder.renderizar(platillo[position])
    }

    class AdaptadorPlatillos(val view:View, var contexto: Context): RecyclerView.ViewHolder(view){

        var platillos_seleccionados = ArrayList<String>()
        var precio_platillos_seleccionados = ArrayList<Int>()

        fun renderizar(platillo: Platos){

            view.lblnombreplatillo.text = platillo.nombre
            view.lbldescripcion.text = platillo.descripcion
            view.lblcosto.text = platillo.precio.toString()
            view.setOnClickListener {

                if(platillo.descripcion == "ACEPTAR"){
                    Toast.makeText(view.context, "ACEPTAR", Toast.LENGTH_SHORT).show()
                    contexto.startActivity(Intent(contexto, Carrito::class.java).
                    putStringArrayListExtra("platillos", platillos_seleccionados))
                }
                Toast.makeText(view.context, platillo.nombre, Toast.LENGTH_SHORT).show()
            }
        }

        private fun Agregar_item_elemento(elemento: String, elementos: ArrayList<String>): ArrayList<String>{

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
}