package com.example.app

import com.google.gson.annotations.SerializedName

data class Users (
    @SerializedName("nombre") val userName: String?,
    @SerializedName("p_apellido") val userFName: String?,
    @SerializedName("s_apellido") val userLName: String?,
    @SerializedName("cedula") val userID: String?,
    //@SerializedName("fecha") val userDate: String?,
    @SerializedName("telefono") val userPhone: String?,
    @SerializedName("contrasena") val userPassword: String?,
    @SerializedName("correo") val userEmail: String?,
    @SerializedName("dia_nacido") val userDay: String?,
    @SerializedName("mes_nacido") val userMonth: String?,
    @SerializedName("anio_nacido") val userAnio: String?,
    @SerializedName("provincia") val userProvincia: String?,
    @SerializedName("canton") val userCanton: String?,
    @SerializedName("distrito") val userDistrito: String?
)
