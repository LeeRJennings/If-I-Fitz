import { useEffect, useState } from "react";
import { getCurrentUsersFavoritedPosts, getCurrentUsersPosts, getPostsByUserId } from "../../modules/postManager";
import { Post } from "./Post";
import { useNavigate } from "react-router-dom";
import { Button, Row, Label } from "reactstrap";
import { getAllUsers, getLoggedInUser } from "../../modules/authManager";
import "./PostViews.css"

export const PostByUser = () => {
    const [posts, setPosts] = useState([])
    const [user, setUser] = useState({})
    const [userFavorites, setUserFavorites] = useState([])
    const [render, setRender] = useState(1)
    const [allUsers, setAllUsers] = useState([])
    
    const navigate = useNavigate()

    const getUser = () => {
        getLoggedInUser()
        .then(user => setUser(user))
    }

    const getUserFavorites = () => {
        getCurrentUsersFavoritedPosts()
        .then(posts => setUserFavorites(posts))
    }

    const getPosts = () => {
        getCurrentUsersPosts()
        .then((posts) => setPosts(posts))
    }

    const getEveryUser = () => {
        getAllUsers()
        .then(users => setAllUsers(users))
    }

    useEffect(() => {
        getUser()
        getPosts()
        getEveryUser()  
    }, [])

    useEffect(() => {
        getUserFavorites()
    }, [render])

    const handleFieldChange = (e) => {
        getPostsByUserId(e.target.value)
        .then(posts => {
            if (!posts.length) {
                window.alert("Sorry, this user doesn't have any posts yet.")
            } else {
                setPosts(posts)
            }
        })
    }

    return (
        <>
        <Label className="m-0 mb-0 mt-2 ms-3" for="users">See other catz boxez:</Label>{" "}
        <select defaultValue="0" name="users" form="categoryForm" onChange={handleFieldChange}>
        <option hidden disabled value="0">--Select a User--</option>
        {allUsers.map(u => (
            <option key={u.id} value={u.id}>
            {u.name}
            </option>
        ))}
        </select>
        <br/>
        <Button className="m-3 mb-0 mt-2" color="success" size="lg" onClick={() =>navigate("/posts/create")}>Add Post</Button>
        <Row className="m-2 mt-1">
            {posts.length ? 
                <>
                {posts?.map((post) => (
                    <Post post={post} 
                          key={post.id} 
                          user={user} 
                          userFavorites={userFavorites} 
                          render={render}
                          setRender={setRender} />
                ))}
                </>
                : <p>You haven't posted any boxez yet! Use the button above to share your first</p>
            }
        </Row>
        </>
    )
}