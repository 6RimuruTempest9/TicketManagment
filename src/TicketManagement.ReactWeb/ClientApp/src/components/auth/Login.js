import React from "react";
import { AppConfiguration } from "read-appsettings-json";
import Cookies from "universal-cookie";

export class Login extends React.Component {
    constructor(props) {
        super(props);

        this.handleSubmit = this.handleSubmit.bind(this);

        this.state = {
            email: "",
            password: "",
        };
    }

    async handleSubmit(e) {
        e.preventDefault();

        const formData = new FormData();

        formData.append("email", this.state.email);
        formData.append("password", this.state.password);

        let response = await fetch(AppConfiguration.Setting().AuthLoginUrl, {
            method: "POST",
            body: formData,
        });

        if (response.ok) {
            const jwt = await response.text();
            const cookies = new Cookies();
            cookies.set("JWT", jwt, { path: "/" });
        }

        this.props.history.push("/events/getall");
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <div>
                    <label for="Email">Email</label>
                    <input
                        type="email"
                        name="Email"
                        value={this.state.email}
                        onChange={(e) => this.setState({
                            email: e.target.value,
                        })}
                    />
                </div>
                <div>
                    <label for="Password">Password</label>
                    <input
                        type="password"
                        name="Password"
                        value={this.state.password}
                        onChange={(e) => this.setState({
                            password: e.target.value,
                        })}
                    />
                </div>
                <div>
                    <input type="submit" value="Login" />
                </div>
            </form>
        );
    }
}