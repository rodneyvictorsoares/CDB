import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CdbService } from '../../services/cdb.service';
import { CDBCalculoResponse } from '../../models/cdb-calculo-response';

@Component({
  selector: 'app-calculator',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css']
})
export class CalculatorComponent {
  valorInicial = 0;
  prazoEmMeses = 1;
  resultado?: CDBCalculoResponse;
  loading = false;
  error = '';

  constructor(private cdbService: CdbService) { }

  errorMessages: string[] = [];
  showErrorModal = false;

  calcular() {
    this.loading = true;
    this.error = '';
    this.resultado = undefined;

    this.cdbService.calcular({
      valorInicial: this.valorInicial,
      prazoEmMeses: this.prazoEmMeses
    }).subscribe({
      next: res => {
        this.resultado = res;
        this.loading = false;
      },
      error: err => {
        this.loading = false;
        const backendErrors = err?.error?.errors;
        if (Array.isArray(backendErrors) && backendErrors.length) {
          this.errorMessages = backendErrors;
        } else {
          const single = err?.error?.error || 'Erro inesperado';
          this.errorMessages = Array.isArray(single) ? single : [single];
        }
        this.showErrorModal = true;
      }
    });
  }

  closeModal() {
    this.showErrorModal = false;
  }

  onValorInicialChange(v: number) {
    this.valorInicial = v < 0 ? 0 : v;
  }

  onPeriodoInicialChange(p: number) {
    this.prazoEmMeses = p < 1 ? 1 : Math.floor(p);
  }
}
