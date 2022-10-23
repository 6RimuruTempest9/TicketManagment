import React from "react";
import { getJwt } from "../Helpers";
import { Link } from "react-router-dom";
import { AppConfiguration } from "read-appsettings-json";

export class Profile extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            user: null,
        }
    }

    async componentDidMount() {
        const jwt = getJwt();
        const formData = new FormData();
        formData.append("jwt", jwt);
        const response = await fetch(AppConfiguration.Setting().GetUserByJwtUrl, {
            method: "POST",
            body: formData,
        });
        const user = await response.json();
        this.setState({
            user: user,
        });
    }

    render() {
        return (
            <table className=".table-borderless">
                <tbody>
                    <tr>
                        <td>Balance: {this.state.user?.balance}</td>
                    </tr>
                    <tr>
                        <td>
                            <Link to="/user/addbalance">Add balance</Link>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <Link to="/user/edit">Edit profile</Link>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <Link to="/user/changePassword">Change password</Link>
                        </td>
                    </tr>
                </tbody>
            </table>
        );
    }
}