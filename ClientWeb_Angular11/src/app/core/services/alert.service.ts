import { Injectable } from '@angular/core';
import { MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition, MatSnackBar } from '@angular/material/snack-bar';

type IAlertType = 'warning' | 'danger' | 'success' | 'info';

interface IAlertOptions {
    duration?: number;
    verticalPosition?: MatSnackBarVerticalPosition;
    horizontalPosition?: MatSnackBarHorizontalPosition;
}

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  constructor(private snackBar: MatSnackBar) { }

  showError(msg: string = 'Error, something went wrong!') {
    this.showAlert(msg, 'danger', '', {});
  }

  showSuccess(msg: string = 'Success, operation finished successfully.') {
    this.showAlert(msg, 'success', '', {});
  }

  showWarning(msg: string) {
    this.showAlert(msg, 'warning', '', {});
  }

  showInfo(msg: string) {
    this.showAlert(msg, 'info', 'Ok', { duration: 10000 });
  }

  showAlert(msg: string, type: IAlertType, action: string = '', opt: IAlertOptions = {}) {
    this.snackBar.open(
      msg,
      action,
      {
        duration: opt.duration || 5000,
        verticalPosition: opt.verticalPosition || 'bottom',
        horizontalPosition: opt.horizontalPosition || 'right',
        panelClass: [`bg-${type}`, 'text-white']
      }
    );
  }
}
