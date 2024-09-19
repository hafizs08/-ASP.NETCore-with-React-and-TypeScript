import { Component, OnInit } from '@angular/core';
import { PlayerService, Player, ApiResponse } from '../../services/player.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-player-form',
  templateUrl: './player-form.component.html',
  styleUrls: ['./player-form.component.css']
})
export class PlayerFormComponent implements OnInit {
  player: Player = { id: 0, name: '', number: 0, position: '' };
  isEditMode: boolean = false;

  constructor(
    private playerService: PlayerService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    const playerId = this.route.snapshot.paramMap.get('id');
    if (playerId) {
      this.isEditMode = true;
      this.playerService.getPlayerById(Number(playerId)).subscribe(
        (response: ApiResponse<Player>) => {
          this.player = response.data;
          this.toastr.success(response.message); // Tampilkan pesan dari backend
        },
        error => this.toastr.error('Gagal mengambil data pemain.')
      );
    }
  }

  onSubmit(form: NgForm): void {
    if (form.invalid) {
      this.toastr.error('Mohon isi semua field dengan benar.');
      return;
    }

    if (this.isEditMode) {
      this.playerService.updatePlayer(this.player).subscribe(
        (response: ApiResponse<Player>) => {
          this.toastr.success(response.message); // Tampilkan pesan dari backend
          this.router.navigate(['/']);
        },
        error => this.toastr.error('Gagal memperbarui pemain.')
      );
    } else {
      this.playerService.addPlayer(this.player).subscribe(
        (response: ApiResponse<Player>) => {
          this.toastr.success(response.message); // Tampilkan pesan dari backend
          this.router.navigate(['/']);
        },
        error => this.toastr.error('Gagal menambahkan pemain.')
      );
    }
  }

  cancel(): void {
    this.router.navigate(['/']);
  }
}
