package com.example.app

import com.google.gson.annotations.SerializedName

data class Seleccion(
        @SerializedName("comida1") val selecComida1: String?,
        @SerializedName("comida2") val selecComida2: String?,
        @SerializedName("comida3") val selecComida3: String?,
        @SerializedName("fecha") val selecDate: String?,
        @SerializedName("telefono") val selecPhone: String?,
        //@SerializedName("contrasena") val userPassword: String?,
        @SerializedName("correo") val userEmail: String?
)