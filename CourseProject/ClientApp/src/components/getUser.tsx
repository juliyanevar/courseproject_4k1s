export async function getUser(){
          const response = await fetch(process.env.REACT_APP_API + "account/GetCurrentUser", { method: "GET"});
          return response.json();
        }