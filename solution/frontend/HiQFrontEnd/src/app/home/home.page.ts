import { Component } from '@angular/core';
import { DocumentService } from '../services/documentservice/document.service';
import { firstValueFrom } from 'rxjs';
import { AlertController, LoadingController } from '@ionic/angular';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
  standalone: false,
})
export class HomePage {

  documentText!: string;

  constructor(private documentService: DocumentService,
              private alertController: AlertController,
              private loadingController: LoadingController
  ) {}


  onFileSelected(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) 
      this.uploadFile(file);
  }  
  
  async uploadFile(file: File)
  {
    const loading = await this.loadingController.create({
      message: 'Laddar upp...',
      spinner: 'crescent'
    });      

    try
    {

      await loading.present();
      var response = await firstValueFrom(this.documentService.uploadFile(file));

      if(response?.error)
        await this.showErrorAlert('Fel',response?.error);  
      else
        this.documentText = response.text;
    }
    catch(error: any)
    {
      this.showErrorAlert('Filuppladdning misslyckades', error.message);
    }
    finally
    {
      await loading.dismiss();
    }
  }

  async showErrorAlert(header: string, message: string) {
    const alert = await this.alertController.create({
      header: header,
      message: message,
      buttons: ['Stäng']
    });
  
    await alert.present();
  }
}
