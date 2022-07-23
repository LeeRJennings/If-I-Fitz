import { getToken } from "./authManager";

const baseUrl = "/api/Size"

export const getAllSizes = () => {
    return getToken().then((token) => {
        return fetch(baseUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then((res) => {
           if (res.ok) {
            return res.json()
           } else {
            throw new Error("An unknown error occurred while trying to get sizes.")
           }
        })
    })
}