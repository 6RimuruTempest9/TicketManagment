import { useHistory } from "react-router-dom";
import { AppConfiguration } from "read-appsettings-json";
import { getJwt } from "../../Helpers";

export function SeatRow(props) {
    const history = useHistory();

    const handleClick = async (e) => {
        const formData = new FormData();

        const jwt = getJwt();

        const formDataForGetUser = new FormData();

        formDataForGetUser.append("jwt", jwt);

        const responseWithUserData = await fetch(AppConfiguration.Setting().GetUserByJwtUrl, {
            method: "POST",
            headers: {
                "Authorization": "Bearer " + jwt,
            },
            body: formDataForGetUser,
        });

        const user = await responseWithUserData.json();

        if (user.balance >= props.price) {
            const formData = new FormData();

            formData.append("id", user.id);
            formData.append("email", user.email);
            formData.append("firstName", user.firstName);
            formData.append("lastName", user.lastName);
            formData.append("language", user.language);
            formData.append("timeZone", user.timeZone);
            formData.append("balance", +user.balance - +props.price);

            await fetch(AppConfiguration.Setting().UpdateUserUrl, {
                method: "POST",
                headers: {
                    "Authorization": "Bearer " + jwt,
                },
                body: formData,
            })

            const formDataForSeatStateUpdate = new FormData();

            formDataForSeatStateUpdate.append("eventSeatState", 1);

            await fetch(AppConfiguration.Setting().UpdateEventSeatStateByIdUrl + props.seat.id, {
                method: "POST",
                body: formDataForSeatStateUpdate,
            })

            const formDataForPurchaseCheck = new FormData();

            formDataForPurchaseCheck.append("userId", user.id);
            formDataForPurchaseCheck.append("ticketPrice", props.price);
            formDataForPurchaseCheck.append("seatId", props.seat.id);

            await fetch("/purchase/buyticket", {
                method: "POST",
                body: formDataForPurchaseCheck,
            })
        }

        history.push("/events/getall");
    };

    let checkBoxElement;

    if (props.seat.state == 0) {
        checkBoxElement = <td><button onClick={handleClick}>Buy ticket</button></td>
    }

    return (
        <tr>
            <td>{props.seat.row}</td>
            <td>{props.seat.number}</td>
            {checkBoxElement}
        </tr>
    );
}