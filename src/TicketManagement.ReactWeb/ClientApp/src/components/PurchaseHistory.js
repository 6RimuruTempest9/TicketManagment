import { useEffect, useState } from "react";
import { AppConfiguration } from "read-appsettings-json";
import { getJwt } from "../Helpers";

export function PurchaseHistory(props) {
    const [history, setHistory] = useState([]);

    useEffect(() => {
        async function getHistory() {
            const jwt = getJwt();
            const formDataUserId = new FormData();
            formDataUserId.append("jwt", jwt);
            const responseUserId = await fetch(AppConfiguration.Setting().GetUserIdByJwtUrl, {
                method: "POST",
                body: formDataUserId,
            });
            const userId = await responseUserId.text();
            const formData = new FormData();
            formData.append("userId", userId);
            const response = await fetch("/purchase/history", {
                method: "POST",
                body: formData,
            });
            const history = await response.json();
            setHistory(history);
        }

        getHistory();
    });

    return (
        <table className="table">
            <thead>
                <tr>
                    <th>Time</th>
                    <th>Amount</th>
                </tr>
            </thead>
            <tbody>
                {history.map(purchase => (
                    <tr>
                        <td>{purchase.time}</td>
                        <td>{purchase.amount}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}