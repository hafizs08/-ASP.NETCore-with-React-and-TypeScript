import { Component, OnInit } from '@angular/core';
import { PlayerService, Player } from '../../services/player.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.css']
})
export class PlayerListComponent implements OnInit {
  players: Player[] = [];

  constructor(private playerService: PlayerService, private router: Router) { }

  ngOnInit(): void {
    this.getPlayers();
  }

  getPlayers(): void {
    this.playerService.getPlayers().subscribe(players => this.players = players);
  }

  deletePlayer(id: number): void {
    this.playerService.deletePlayer(id).subscribe(() => {
      this.players = this.players.filter(p => p.id !== id);
    });
  }


  addNewPlayer(): void {
    this.router.navigate(['/add-player']);
  }

  editPlayer(player: Player): void {
    this.router.navigate(['/edit-player', player.id]);
  }

}

