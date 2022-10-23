import React from "react";
import { AppConfiguration } from "read-appsettings-json";
import { getJwt } from "../Helpers";

export class EditProfile extends React.Component {
    constructor(props) {
        super(props);

        this.handleSubmit = this.handleSubmit.bind(this);

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
        })
        const user = await response.json();
        this.setState({ user: user });
    }

    async handleSubmit(e) {
        e.preventDefault();

        const jwt = getJwt();

        const formData = new FormData();

        formData.append("id", this.state.user.id);
        formData.append("email", this.state.user.email);
        formData.append("firstName", this.state.user.firstName);
        formData.append("lastName", this.state.user.lastName);
        formData.append("language", this.state.user.language);
        formData.append("timeZone", this.state.user.timeZone);
        formData.append("balance", this.state.user.balance);

        await fetch(AppConfiguration.Setting().UpdateUserUrl, {
            method: "POST",
            headers: {
                "Authorization": "Bearer " + jwt,
            },
            body: formData,
        })

        this.props.history.push("/user/profile");
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <div>
                    <label for="FirstName">First name</label>
                    <input
                        type="text"
                        name="FirstName"
                        value={this.state.user?.firstName}
                        onChange={(e) => {
                            const newUser = { ...this.state.user };
                            newUser.firstName = e.target.value;
                            this.setState({ user: newUser });
                        }}
                    />
                </div>
                <div>
                    <label for="LastName">Last name</label>
                    <input
                        type="text"
                        name="LastName"
                        value={this.state.user?.lastName}
                        onChange={(e) => {
                            const newUser = { ...this.state.user };
                            newUser.lastName = e.target.value;
                            this.setState({ user: newUser });
                        }}
                    />
                </div>
                <div>
                    <label for="Email">Email</label>
                    <input
                        type="text"
                        name="Email"
                        value={this.state.user?.email}
                        onChange={(e) => {
                            const newUser = { ...this.state.user };
                            newUser.email = e.target.value;
                            this.setState({ user: newUser });
                        }}
                    />
                </div>
                <div>
                    <label for="Language">Language</label>
                    <select name="Language" value={this.state.user?.language} onChange={(e) => {
                        const newUser = { ...this.state.user };
                        newUser.language = e.target.value;
                        this.setState({ user: newUser });
                    }}>
                        <option value="Russian">Russian</option>
                        <option value="English">English</option>
                        <option value="Belarussian">Belarussian</option>
                    </select>
                </div>
                <div>
                    <label for="TimeZone">Time zone</label>
                    <select name="TimeZone" value={this.state.user?.timeZone} onChange={(e) => {
                        const newUser = { ...this.state.user };
                        newUser.timeZone = e.target.value;
                        this.setState({ user: newUser });
                    }}>
                        <option>(UTC-12:00) Линия перемены дат</option>
                        <option>(UTC-11:00) Время в формате UTC -11</option>
                        <option>(UTC-10:00) Алеутские острова</option>
                        <option>(UTC-10:00) Гавайи</option>
                        <option>(UTC-09:30) Маркизские острова</option>
                        <option>(UTC-09:00) Аляска</option>
                        <option>(UTC-09:00) Время в формате UTC -09</option>
                        <option>(UTC-08:00) Время в формате UTC -08</option>
                        <option>(UTC-08:00) Нижняя Калифорния</option>
                        <option>(UTC-08:00) Тихоокеанское время (США и Канада)</option>
                        <option>(UTC-07:00) Аризона</option>
                        <option>(UTC-07:00) Горное время (США и Канада)</option>
                        <option>(UTC-07:00) Ла-Пас, Мазатлан, Чихуахуа</option>
                        <option>(UTC-07:00) Юкон</option>
                        <option>(UTC-06:00) Гвадалахара, Мехико, Монтеррей</option>
                        <option>(UTC-06:00) Саскачеван</option>
                        <option>(UTC-06:00) Центральная Америка</option>
                        <option>(UTC-06:00) Центральное время (США и Канада)</option>
                        <option>(UTC-06:00) о. Пасхи</option>
                        <option>(UTC-05:00) Богота, Кито, Лима, Рио-Бранко</option>
                        <option>(UTC-05:00) Восточное время (США и Канада)</option>
                        <option>(UTC-05:00) Гавана</option>
                        <option>(UTC-05:00) Гаити</option>
                        <option>(UTC-05:00) Индиана (восток)</option>
                        <option>(UTC-05:00) Острова Теркс и Кайкос</option>
                        <option>(UTC-05:00) Четумаль</option>
                        <option>(UTC-04:00) Асунсьон</option>
                        <option>(UTC-04:00) Атлантическое время (Канада)</option>
                        <option>(UTC-04:00) Джорджтаун, Ла-Пас, Манаус, Сан-Хуан</option>
                        <option>(UTC-04:00) Каракас</option>
                        <option>(UTC-04:00) Куяба</option>
                        <option>(UTC-04:00) Сантьяго</option>
                        <option>(UTC-03:30) Ньюфаундленд</option>
                        <option>(UTC-03:00) Арагуаяна</option>
                        <option>(UTC-03:00) Арагуаяна</option>
                        <option>(UTC-03:00) Буэнос-Айрес</option>
                        <option>(UTC-03:00) Гренландия</option>
                        <option>(UTC-03:00) Кайенна, Форталеза</option>
                        <option>(UTC-03:00) Монтевидео</option>
                        <option>(UTC-03:00) Пунта-Аренас</option>
                        <option>(UTC-03:00) Сальвадор</option>
                        <option>(UTC-03:00) Сен-Пьер и Микелон</option>
                        <option>(UTC-02:00) Время в формате UTC -02</option>
                        <option>(UTC-02:00) Среднеатлантическое время - старое</option>
                        <option>(UTC-01:00) Азорские о-ва</option>
                        <option>(UTC-01:00) Кабо-Верде</option>
                        <option>(UTC) Coordinated Universal Time</option>
                        <option>(UTC+00:00) Дублин, Эдинбург, Лиссабон, Лондон</option>
                        <option>(UTC+00:00) Монровия, Рейкьявик</option>
                        <option>(UTC+00:00) Сан-Томе</option>
                        <option>(UTC+01:00) Касабланка</option>
                        <option>(UTC+01:00) Амстердам, Берлин, Берн, Вена, Рим, Стокгольм</option>
                        <option>(UTC+01:00) Белград, Братислава, Будапешт, Любляна, Прага</option>
                        <option>(UTC+01:00) Брюссель, Копенгаген, Мадрид, Париж</option>
                        <option>(UTC+01:00) Варшава, Загреб, Сараево, Скопье</option>
                        <option>(UTC+01:00) Варшава, Загреб, Сараево, Скопье</option>
                        <option>(UTC+02:00) Амман</option>
                        <option>(UTC+02:00) Афины, Бухарест</option>
                        <option>(UTC+02:00) Бейрут</option>
                        <option>(UTC+02:00) Вильнюс, Киев, Рига, София, Таллин, Хельсинки</option>
                        <option>(UTC+02:00) Виндхук</option>
                        <option>(UTC+02:00) Дамаск</option>
                        <option>(UTC+02:00) Джуба</option>
                        <option>(UTC+02:00) Иерусалим</option>
                        <option>(UTC+02:00) Каир</option>
                        <option>(UTC+02:00) Калининград</option>
                        <option>(UTC+02:00) Кишинев</option>
                        <option>(UTC+02:00) Сектор Газа, Хеврон</option>
                        <option>(UTC+02:00) Триполи</option>
                        <option>(UTC+02:00) Хараре, Претория</option>
                        <option>(UTC+02:00) Хартум</option>
                        <option>(UTC+03:00) Багдад</option>
                        <option>(UTC+03:00) Волгоград</option>
                        <option>(UTC+03:00) Кувейт, Эр-Рияд</option>
                        <option>(UTC+03:00) Минск</option>
                        <option>(UTC+03:00) Москва, Санкт-Петербург</option>
                        <option>(UTC+03:00) Найроби</option>
                        <option>(UTC+03:00) Стамбул</option>
                        <option>(UTC+03:30) Тегеран</option>
                        <option>(UTC+04:00) Абу-Даби, Мускат</option>
                        <option>(UTC+04:00) Астрахань, Ульяновск</option>
                        <option>(UTC+04:00) Баку</option>
                        <option>(UTC+04:00) Ереван</option>
                        <option>(UTC+04:00) Ижевск, Самара</option>
                        <option>(UTC+04:00) Порт-Луи</option>
                        <option>(UTC+04:00) Саратов</option>
                        <option>(UTC+04:00) Тбилиси</option>
                        <option>(UTC+04:30) Кабул</option>
                        <option>(UTC+05:00) Ашхабад, Ташкент</option>
                        <option>(UTC+05:00) Екатеринбург</option>
                        <option>(UTC+05:00) Исламабад, Карачи</option>
                        <option>(UTC+05:00) Кызылорда</option>
                        <option>(UTC+05:30) Колката, Мумбаи, Нью-Дели, Ченнай</option>
                        <option>(UTC+05:30) Шри-Джаявардене-пура-Котте</option>
                        <option>(UTC+05:45) Катманду</option>
                        <option>(UTC+06:00) Астана</option>
                        <option>(UTC+06:00) Дакка</option>
                        <option>(UTC+06:00) Омск</option>
                        <option>(UTC+06:30) Янгон</option>
                        <option>(UTC+07:00) Бангкок, Джакарта, Ханой</option>
                        <option>(UTC+07:00) Барнаул, Горно-Алтайск</option>
                        <option>(UTC+07:00) Красноярск</option>
                        <option>(UTC+07:00) Новосибирск</option>
                        <option>(UTC+07:00) Томск</option>
                        <option>(UTC+07:00) Ховд</option>
                        <option>(UTC+08:00) Гонконг, Пекин, Урумчи, Чунцин</option>
                        <option>(UTC+08:00) Иркутск</option>
                        <option>(UTC+08:00) Куала-Лумпур, Сингапур</option>
                        <option>(UTC+08:00) Перт</option>
                        <option>(UTC+08:00) Тайбэй</option>
                        <option>(UTC+08:00) Улан-Батор</option>
                        <option>(UTC+08:45) Юкла</option>
                        <option>(UTC+09:00) Осака, Саппоро, Токио</option>
                        <option>(UTC+09:00) Пхеньян</option>
                        <option>(UTC+09:00) Сеул</option>
                        <option>(UTC+09:00) Чита</option>
                        <option>(UTC+09:00) Якутск</option>
                        <option>(UTC+09:30) Аделаида</option>
                        <option>(UTC+09:30) Дарвин</option>
                        <option>(UTC+10:00) Брисбен</option>
                        <option>(UTC+10:00) Владивосток</option>
                        <option>(UTC+10:00) Гуам, Порт-Морсби</option>
                        <option>(UTC+10:00) Канберра, Мельбурн, Сидней</option>
                        <option>(UTC+10:00) Хобарт</option>
                        <option>(UTC+10:30) Лорд-Хау</option>
                        <option>(UTC+11:00) Магадан</option>
                        <option>(UTC+11:00) Остров Бугенвиль</option>
                        <option>(UTC+11:00) Остров Норфолк</option>
                        <option>(UTC+11:00) Сахалин</option>
                        <option>(UTC+11:00) Соломоновы о-ва, Нов. Каледония</option>
                        <option>(UTC+11:00) Чокурдах</option>
                        <option>(UTC+12:00) Анадырь, Петропавловск-Камчатский</option>
                        <option>(UTC+12:00) Веллингтон, Окленд</option>
                        <option>(UTC+12:00) Время в формате UTC +12</option>
                        <option>(UTC+12:00) Петропавловск-Камчатский — устаревшее</option>
                        <option>(UTC+12:00) Фиджи</option>
                        <option>(UTC+12:45) Чатем</option>
                        <option>(UTC+13:00) Время в формате UTC +13</option>
                        <option>(UTC+13:00) Нукуалофа</option>
                        <option>(UTC+13:00) Самоа</option>
                        <option>(UTC+14:00) О-в Киритимати</option>
                    </select>
                </div>
                <div>
                    <input type="submit" value="Edit" />
                </div>
            </form>
        );
    }
}