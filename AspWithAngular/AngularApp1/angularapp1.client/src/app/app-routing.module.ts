import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PlayerListComponent } from './components/player-list/player-list.component';
import { PlayerFormComponent } from './components/player-form/player-form.component';

const routes: Routes = [
  { path: '', component: PlayerListComponent },
  { path: 'add-player', component: PlayerFormComponent },
  { path: 'edit-player/:id', component: PlayerFormComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
