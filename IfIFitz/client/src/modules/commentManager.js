import { getToken } from "./authManager";

const baseUrl = "/api/Comment"

export const getCommentsByPostId = (id) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/Post/${id}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((res) => {
            if (res.ok) {
                return res.json()
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get comments."
                )
            }
        })
    })
}

export const addComment = (comment) => {
    return getToken().then((token) => {
        return fetch(baseUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json",
            },
            body: JSON.stringify(comment),
        }).then((res) => {
            if (res.ok) {
                return res.json()
            } else {
                throw new Error("An unknown error occurred while trying to save your post.")
            }
        })
    })
}