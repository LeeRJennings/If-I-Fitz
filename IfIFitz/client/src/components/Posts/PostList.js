import { useEffect, useState } from "react";
import { getAllPosts, getCurrentUsersFavoritedPosts } from "../../modules/postManager";
import { Post } from "./Post";
import { useNavigate } from "react-router-dom";
import { Button, Row } from "reactstrap";
import { getLoggedInUser } from "../../modules/authManager";

export const PostList = () => {
    const [posts, setPosts] = useState([])
    const [user, setUser] = useState({})
    const [userFavorites, setUserFavorites] = useState([])
    const [render, setRender] = useState(1)

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
        getAllPosts()
        .then(posts => setPosts(posts))
    }

    useEffect(() => {
        getUser()
        getPosts()
    }, [])

    useEffect(() => {
        getUserFavorites()
    }, [render])

    return (
        <>
        <Button color="success" size="lg" onClick={() => navigate("/posts/create")}>Add Post</Button>
        <Row>
            {posts.map((post) => (
                <Post post={post} 
                      key={post.id} 
                      user={user} 
                      userFavorites={userFavorites} 
                      render={render}
                      setRender={setRender} />
            ))}
        </Row>
        </>
    )
}