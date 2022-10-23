import React from "react";
import { Link } from "react-router-dom";
import { AppConfiguration } from "read-appsettings-json";

export class AreaRow extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            seats: [],
        }
    }

    async componentDidMount() {
        const response = await fetch(AppConfiguration.Setting().GetEventsByAreaIdUrl + this.props.area.id);

        this.setState({
            seats: await response.json(),
        });
    }

    render() {
        return (
            <tr>
                <td>{this.props.area.description}</td>
                <td>{this.props.area.coordX}</td>
                <td>{this.props.area.coordY}</td>
                <td>{this.props.area.price}</td>
                <td>
                    <Link to={{ pathname: "/eventseats/getallbyeventareaid", seats: this.state.seats, price: this.props.area.price }}>More info</Link>
                </td>
            </tr>
        );
    }
}