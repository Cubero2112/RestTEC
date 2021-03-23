import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./componentes/login/login.component";
import {GestionMenuComponent} from "./componentes/admin/gestion-menu/gestion-menu.component";
import {ReportesComponent} from "./componentes/admin/reportes/reportes.component";
import {GestionPlatosComponent} from "./componentes/admin/gestion-platos/gestion-platos.component";
import {MisPedidosComponent} from "./componentes/chef/mis-pedidos/mis-pedidos.component";
import {ControlComponent} from "./componentes/chef/control/control.component";
import {AsignarComponent} from "./componentes/chef/asignar/asignar.component";


const routes: Routes = [
{path: '', component:LoginComponent},
{path: 'admin/menu', component:GestionMenuComponent},
{path: 'admin/reportes', component:ReportesComponent},
{path: 'admin/platos', component:GestionPlatosComponent},
{path: 'chef/pedidos', component:MisPedidosComponent},
{path: 'chef/control', component:ControlComponent},
{path: 'chef/asignar', component:AsignarComponent},
{path: '**', pathMatch: 'full', redirectTo: ''}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
