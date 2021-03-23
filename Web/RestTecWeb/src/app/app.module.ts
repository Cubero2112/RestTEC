import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import {HttpClientModule} from '@angular/common/http';
import {FormsModule} from "@angular/forms";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GestionMenuComponent } from './componentes/admin/gestion-menu/gestion-menu.component';
import { GestionPlatosComponent } from './componentes/admin/gestion-platos/gestion-platos.component';
import { ReportesComponent } from './componentes/admin/reportes/reportes.component';
import { NavbarAdminComponent } from './componentes/admin/navbar-admin/navbar-admin.component';
import { AsignarComponent } from './componentes/chef/asignar/asignar.component';
import { ControlComponent } from './componentes/chef/control/control.component';
import { MisPedidosComponent } from './componentes/chef/mis-pedidos/mis-pedidos.component';
import { NavbarChefComponent } from './componentes/chef/navbar-chef/navbar-chef.component';
import { LoginComponent } from './componentes/login/login.component';
import { ModalPlatosComponent } from './componentes/admin/modal-platos/modal-platos.component';

@NgModule({
  declarations: [
    AppComponent,
    GestionMenuComponent,
    GestionPlatosComponent,
    ReportesComponent,
    NavbarAdminComponent,
    AsignarComponent,
    ControlComponent,
    MisPedidosComponent,
    NavbarChefComponent,
    LoginComponent,
    ModalPlatosComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
