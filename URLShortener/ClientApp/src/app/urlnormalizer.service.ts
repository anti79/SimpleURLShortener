import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class URLNormalizerService {

  urlPattern = /^https?:\/\/(?:www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b(?:[-a-zA-Z0-9()@:%_\+.~#?&\/=]*)$/
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
