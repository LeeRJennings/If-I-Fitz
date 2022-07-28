import { useEffect, useState } from "react";
import { getCurrentUsersPosts } from "../../modules/postManager";
import { Post } from "./Post";
import { useNavigate } from "react-router-dom";
import { Button, Row } from "reactstrap";
import { getLoggedInUser } from "../../modules/authManager";

export const PostByUser = () => {
    const [posts, setPosts] = useState([])
    const [user, setUser] = useState({})
    
    const navigate = useNavigate()

    const getUser = () => {
        getLoggedInUser()
        .then(user => setUser(user))
    }

    const getPosts = () => {
        getCurrentUsersPosts()
        .then((posts) => setPosts(posts))
    }

    useEffect(() => {
        getUser()
        getPosts()  
    }, [])

    return (
        <>
        <Button color="success" size="lg" onClick={() =>navigate("/posts/create")}>Add Post</Button>
        <Row>
            {posts.map((post) => (
                <Post post={post} 
                      key={post.id} 
                      user={user} />
            ))}
        </Row>
        </>
    )
}