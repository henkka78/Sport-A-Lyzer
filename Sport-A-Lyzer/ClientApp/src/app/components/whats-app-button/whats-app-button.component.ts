import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-whats-app-button',
  template: '<a href="whatsapp://send?text=www.bloodyhanks.com/game/{{gameId}}" class="mx-auto"><mdb-icon fab icon="whatsapp-square" class="green-text pr-3" size="4x"></mdb-icon></a>',
  styleUrls: ['./whats-app-button.component.scss']
})
export class WhatsAppButtonComponent {
  @Input() gameId: string;


}
