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

export const getCommentById = (id) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/${id}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((res) => {
            if (res.ok) {
                return res.json()
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get this comment."
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
                throw new Error("An unknown error occurred while trying to save your comment.")
            }
        })
    })
}

export const updateComment = (comment) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/${comment.id}`, {
            method: "PUT",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(comment)
        }).then((res) => {
            if (res.ok) {  
            }
            else if (res.status === 401) {
                throw new Error("Unauthorized");
            }
            else {
                throw new Error("An unknown error occurred while trying to edit comment.")
            }
        })
    })
}

export const deleteComment = (id) => {
    return getToken().then((token) => {
        return fetch(`${baseUrl}/${id}`, {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${token}`
            },
            body: JSON.stringify(id)
        }).then((res) => {
            if (res.ok) {
            } else if (res.status === 401) {
                throw new Error("Unauthorized")
            } else {
                throw new Error(
                    "An unknown error occured while trying to delete a comment."
                )
            }
        })
    })
}