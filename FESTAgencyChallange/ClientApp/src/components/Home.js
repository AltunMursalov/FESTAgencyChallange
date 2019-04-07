import React, { Component } from 'react';
import { throws } from 'assert';

export class Home extends Component {
    displayName = Home.name

    constructor() {
        super();
        this.state = {
            zipCode: '',
            isMetric: false,
            message: ''
        };
        this.getInfo = this.getInfo.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }

    updateInputValue(evt) {
        this.setState({
            zipCode: evt.target.value
        });
    }

    handleChange() {
      this.setState({
          isMetric: !this.state.isMetric      
      })
    }

    getInfo() {
        fetch(`api/Weather?zipCode=${this.state.zipCode}&isMetric=${this.state.isMetric}`,
            {
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                }
            })
            .then((response) => {
              if(!response.ok){
                return alert(`Zip code ${this.state.zipCode} cannot be found!`)
              }
              return response.json()
            })
            .then(data => {
              if(data === undefined)
                return;
              let measure = 'F'
              let gmt = "GMT+"
              if(data.gmt < 0)
                gmt = "GMT"
              if(this.state.isMetric)
                measure = 'C'
              this.setState({
                message: `At the location ${data.city}, the temperature is ${data.temperature}${measure},
                and the timezone is ${data.timeZone} ${gmt}${data.gmt}`
              })
            })
    }

    render() {
        return (
            <div>
                <h1>Hello, this is front end application for FEST Agency Challange!</h1>
                <p>Built with:</p>
                <ul>
                    <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
                    <li><a href='https://facebook.github.io/react/'>React</a> for client-side code</li>
                    <li><a href='https://github.com/AltunMursalov/FESTAgencyChallange'>Source code</a> - Link to GitHub repository with source code</li>
                </ul>
                <p>Enter a zip code for get info about weather and time. For example - 2016,au or 94040,us or 123001,ru etc:</p>
                <input type="checkbox" checked={this.state.isMetric} onChange={this.handleChange}/> Show data in metric measure <br/>
                <input type="text" placeholder="Zip code" value={this.state.zipCode} onChange={evt => this.updateInputValue(evt)} />
                <button onClick={this.getInfo}>Click</button>
                <p>{this.state.message}</p>
            </div>
        );
    }
}
