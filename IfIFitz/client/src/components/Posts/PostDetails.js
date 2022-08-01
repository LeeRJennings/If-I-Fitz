import { useState, useEffect } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { getCurrentUsersFavoritedPosts, getPostById } from "../../modules/postManager"
import { getCommentsByPostId } from "../../modules/commentManager"
import { Button, Row } from "reactstrap"
import { Comment } from "../Comments/Comment"
import { getLoggedInUser } from "../../modules/authManager"
import { Post } from "./Post"

export const PostDetails = () => {
    const [user, setUser] = useState({})
    const [post, setPost] = useState({
        title: "",
        createdDateTime: "",
        imageLocation: "",
        description: "",
        userProfile: {
            name: ""
        },
        size: {
            name: ""
        },
        material: {
            type: ""
        }
    })
    const [comments, setComments] = useState([])
    const [userFavorites, setUserFavorites] = useState([])
    const [render, setRender] = useState(1)
    const [commentRender, setCommentRender] = useState(1)

    const navigate = useNavigate()
    const {id} = useParams()

    const getUser = () => {
        getLoggedInUser()
        .then(user => setUser(user))
    }

    const getUserFavorites = () => {
        getCurrentUsersFavoritedPosts()
        .then(posts => setUserFavorites(posts))
    }

    const getPost = () => {
        getPostById(id)
        .then(post => setPost(post))
    }

    const getComments = () => {
        getCommentsByPostId(id)
        .then(comments => setComments(comments))
    }

    useEffect(() => {
        getUser()
        getPost()
    }, [])

    useEffect(() => {
        getUserFavorites()
    }, [render])

    useEffect(() => {
        getComments()
    }, [commentRender])

    const commentCheck = () => {
        if (comments.length) {
            return (
                <>
                <Row className="ms-1">
                {comments.map((comment) => (
                    
                    <Comment comment={comment} 
                             key={comment.id} 
                             user={user} 
                             commentRender={commentRender} 
                             setCommentRender={setCommentRender}/>
                    ))}
                </Row>
                </>
            )
        } else {
            return (
                <><p>No comments for this post yet. You can add the first!</p></>
            )
        }
    }
    
    return (
        <div id="postDetails">
            <Post post={post} 
                  user={user} 
                  key={post.id} 
                  userFavorites={userFavorites} 
                  render={render}
                  setRender={setRender} />
            <Button className="m-2 ms-2" color="success" size="lg" onClick={() => navigate(`/posts/${post.id}/addComment`)}>Add Comment</Button>
            {commentCheck()}
        </div>
    )
}