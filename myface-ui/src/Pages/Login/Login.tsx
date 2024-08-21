import React, {FormEvent, useContext, useState} from 'react';
import {Page} from "../Page/Page";
import {LoginContext} from "../../Components/LoginManager/LoginManager";
import "./Login.scss";

async function loginUser(credentials: { username: string; password: string; }) {
    // return fetch("http://localhost:5001", {
    //     method: "POST",
    //     headers: {
    //         "Content-Type" : "application/json", 
    //         // 'Authorization': 'Basic' + (credentials.username + ":" + credentials.password).toString('base64')
    //         'Authorization': 'Basic ' + btoa(credentials.username + ":" + credentials.password)
    //         //"Authorization": `Basic ${base64.encode(`${credentials.username}:${credentials.password}`)}`
    //     },
    //     body : JSON.stringify(credentials)
    // })
    // //.then(data => data.json())
    // .then(response => response.status)
    let data = await fetch("http://localhost:5001", {
        method: "POST",
        headers: {'Authorization': 'Basic ' + btoa(credentials.username + ":" + credentials.password)},
        body : JSON.stringify(credentials)
    })
    let response = await data.json();
    return response;
    
}

export function Login(): JSX.Element {
    const loginContext = useContext(LoginContext);
    
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    
    
    async function tryLogin(event: FormEvent) {
        event.preventDefault();

        const response = await loginUser({username,password});
        if (response == 200){
            loginContext.logIn();
            console.log("Logging IN")
        } else
        {
            loginContext.logOut();
            console.log("Logging OUT")
        }
    }

    return (
        <Page containerClassName="login">
            <h1 className="title">Log In</h1>
            <form className="login-form" onSubmit={tryLogin}>
                <label className="form-label">
                    Username
                    <input className="form-input" type={"text"} value={username} onChange={event => setUsername(event.target.value)}/>
                </label>

                <label className="form-label">
                    Password
                    <input className="form-input" type={"password"} value={password} onChange={event => setPassword(event.target.value)}/>
                </label>
                
                <button className="submit-button" type="submit">Log In</button>
            </form>
        </Page>
    );
}