import React, {FormEvent, useContext, useState} from 'react';
import {Page} from "../Page/Page";
import {LoginContext} from "../../Components/LoginManager/LoginManager";
import "./Login.scss";

async function loginUser(credentials: { username: string; password: string; }) {
    // return fetch("https://localhost:5001", {
    //     method: "get",
    //     headers: {
    //         "Content-Type" : "application/json", 
    //         // 'Authorization': 'Basic' + (credentials.username + ":" + credentials.password).toString('base64')
    //         'Authorization': 'Basic ' + btoa(credentials.username + ":" + credentials.password)
    //         //"Authorization": `Basic ${base64.encode(`${credentials.username}:${credentials.password}`)}`
    //     },
    // })
    // //.then(data => data.json())
    // .then(response => response.status)

    // let response = null
    // while (response == null) {
    //     response = await fetch("https://localhost:5001", {
    //         method: "get",
    //         headers: {'Authorization': 'Basic ' + btoa(credentials.username + ":" + credentials.password)},
    //        mode:  'no-cors', // in mode="cors", if wrong username/password the fetch is blocked and we cannot read the response=401 
    //     })
    //     if (!response.ok) {
    //         console.log("response not OK")
    //         console.log(response.status)
    //     }
    // }
    // return response.status;
    // console.log("Going to fetch:")

    try{
        let response = await fetch("https://localhost:5001", {
            method: "get",
            headers: {'Authorization': 'Basic ' + btoa(credentials.username + ":" + credentials.password)},
           //mode:  'no-cors', // in mode="cors", if wrong username/password the fetch is blocked and we cannot read the response=401 
        })
        return response.status;
    } catch(err){
        console.log("error in fetch");
        console.log(err);
        return 0;
    }

    
        // let response = await fetch("https://localhost:5001", {
        //     method: "get",
        //     headers: {'Authorization': 'Basic ' + btoa(credentials.username + ":" + credentials.password)},
        //    mode:  'no-cors', // in mode="cors", if wrong username/password the fetch is blocked and we cannot read the response=401 
        // })
        // if (response.status != 200) {
        //     console.log("response not OK")
        //     console.log(response.status)
        // }
        // return response.status;
    
}

export function Login(): JSX.Element {
    const loginContext = useContext(LoginContext);
    
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    
    
    async function tryLogin(event: FormEvent) {
        event.preventDefault();
        console.log("in TryLogin")
        const response = await loginUser({username,password});
        //console.log(response)
        if (response === 200){
            loginContext.logIn();
            console.log("Logging IN")
        } else {
            //console.log(response);
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