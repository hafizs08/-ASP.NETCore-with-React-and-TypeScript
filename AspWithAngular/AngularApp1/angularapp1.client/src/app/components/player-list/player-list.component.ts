import { Component, OnInit } from '@angular/core';
import { PlayerService, Player } from '../../services/player.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.css']
})
export class PlayerListComponent implements OnInit {
  players: Player[] = [];

  constructor(
    private playerService: PlayerService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.getPlayers();
  }

  getPlayers(): void {
    this.playerService.getPlayers().subscribe(
      players => this.players = players,
      error => this.toastr.error('Gagal mengambil data pemain.')
    );
  }

  deletePlayer(id: number): void {
    if (confirm('Apakah Anda yakin ingin menghapus pemain ini?')) {
      this.playerService.deletePlayer(id).subscribe(() => {
        this.players = this.players.filter(p => p.id !== id);
        this.toastr.success('Pemain berhasil dihapus.');
      }, error => {
        this.toastr.error('Gagal menghapus pemain.');
      });
    }
  }

  addNewPlayer(): void {
    this.router.navigate(['/add-player']);
  }

  editPlayer(player: Player): void {
    this.router.navigate(['/edit-player', player.id]);
  }
}
