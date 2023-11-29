import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class URLNormalizerService {

  urlPattern = /^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$/
  public normalize(url: string): string {

    url = url.toLowerCase();
    if (!url.startsWith("http://") && !url.startsWith("https://")) {
      url = "https://" + url;
    }
    url = decodeURIComponent(url);
    url = url.replace(/:\d+\//, '/'); //remove port

    // remove redundant slashes
    url = new URL(url).href;
      if (this.urlPattern.test(url)) {
        return url;
      } 
      else {
        throw new URIError("Invalid URL");
      }
  }


  constructor() { }
}
