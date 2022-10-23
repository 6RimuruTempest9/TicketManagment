import { SeatRow } from "./SeatRow";

export function SeatsTable(props) {
    return (
        <table className="table">
            <thead>
                <tr>
                    <th>Row</th>
                    <th>Number</th>
                </tr>
            </thead>
            <tbody>
                {props.location.seats.map(seat => <SeatRow key={seat.id} seat={seat} price={props.location.price} />)}
            </tbody>
        </table>
    );
}