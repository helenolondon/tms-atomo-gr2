import { Component, OnInit } from '@angular/core';


declare let paypal: any;

@Component({
  selector: 'botao-pagamento',
  templateUrl: './botao-pagamento.component.html',
  styleUrls: ['./botao-pagamento.component.scss']
})
export class BotaoPagamentoComponent implements OnInit {

  constructor() { }

  private loadExternalScript(scriptUrl: string) {
    return new Promise((resolve, reject) => {
      const scriptElement = document.createElement('script')
      scriptElement.src = scriptUrl
      scriptElement.onload = resolve
      document.body.appendChild(scriptElement)
    })
  }

  ngAfterViewInit(): void {
    this.loadExternalScript("https://www.paypal.com/sdk/js?client-id=sb&enable-funding=venmo&currency=USD").then(() => {
      function initPayPalButton() {
        paypal.Buttons({
          style: {
            shape: 'rect',
            color: 'gold',
            layout: 'vertical',
            label: 'paypal',
            
          },
  
          createOrder: function(data, actions) {
            return actions.order.create({
              purchase_units: [{"amount":{"currency_code":"USD","value":1}}]
            });
          },
  
          onApprove: function(data, actions) {
            return actions.order.capture().then(function(orderData) {
              
              // Full available details
              console.log('Capture result', orderData, JSON.stringify(orderData, null, 2));
  
              // Show a success message within this page, e.g.
              const element = document.getElementById('paypal-button-container');
              element.innerHTML = '';
              element.innerHTML = '<h3>Thank you for your payment!</h3>';
  
              // Or go to another URL:  actions.redirect('thank_you.html');
              
            });
          },
  
          onError: function(err) {
            console.log(err);
          }
        }).render('#paypal-button-container');
      }
      initPayPalButton();
    });
  }

  ngOnInit() {
  }    

}
