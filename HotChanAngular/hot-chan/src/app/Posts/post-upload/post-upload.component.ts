import { AfterViewChecked, AfterViewInit, ChangeDetectionStrategy, Component, OnChanges, OnInit, ViewChild } from '@angular/core';
import { GalleryComponent, GalleryItem, ImageItem } from 'ng-gallery';

@Component({
  selector: 'app-post-upload',
  templateUrl: './post-upload.component.html',
  styleUrls: ['./post-upload.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PostUploadComponent implements OnInit, AfterViewInit  {

  constructor() { }

  //@ViewChild('#fileSelector') fileSelector!: HTMLInputElement;
  @ViewChild(GalleryComponent) gallery!: GalleryComponent;

  images: GalleryItem[] = [];

  ngOnInit() {
    
  }

  ngAfterViewInit(): void {
    
  }

  fileChange(event: any) {
    let fileList: FileList = event.target.files;
    
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();
      reader.onload = (event: any) => {
          console.log(event.target.result);
          const item = {data: event.target.result, type: 'jpg'};
          this.gallery.addImage({ src: event.target.result, thumb: event.target.result });
      }
      reader.readAsDataURL(event.target.files[0]);
  }

    // if(fileList.length > 0) {
    //     let file: File = fileList[0];
    //     let formData:FormData = new FormData();
    //     formData.append('uploadFile', file, file.name);
    //     let headers = new Headers();
        
    //     headers.append('Content-Type', 'multipart/form-data');
    //     headers.append('Accept', 'application/json');
        //let options = new RequestOptions({ headers: headers });
        // this.http.post(`${this.apiEndPoint}`, formData, options)
        //     .map(res => res.json())
        //     .catch(error => Observable.throw(error))
        //     .subscribe(
        //         data => console.log('success'),
        //         error => console.log(error)
        //     )
    //}
  }
}
