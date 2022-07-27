import { useState, useEffect } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { getPostById } from "../../modules/postManager"
import { getCommentsByPostId } from "../../modules/commentManager"
import { addFavorite, deleteFavorite } from "../../modules/postManager"
import { Button, Card, CardBody, CardTitle, CardSubtitle, CardText } from "reactstrap"
import { Comment } from "../Comments/Comment"

export const PostDetails = ({ user, userFavorites, render, setRender }) => {
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

    const navigate = useNavigate()
    const {id} = useParams()

    const getPost = () => {
        getPostById(id)
        .then(post => setPost(post))
    }

    const getComments = () => {
        getCommentsByPostId(id)
        .then(comments => setComments(comments))
    }

    const handleAddFavorite = (id) => {
        addFavorite(id)
        .then(() => setRender(render + 1))
    }

    const handleDeleteFavorite = (id) => {
        deleteFavorite(id)
        .then(() => setRender(render + 1))
    }

    const buttonDisplay = () => {
        if (user.id === post.userProfileId) {
            return (
                <>
                <Button color="primary" onClick={() => navigate(`/posts/edit/${post.id}`)}>Edit</Button>
                <Button color="danger" onClick={() => navigate(`/posts/delete/${post.id}`)}>Delete</Button>
                </>
            )
        } else {
            if (userFavorites.find((f) => f.id === post.id)) {
                return (
                    <div type="button" className="bggray2 text-info">
                        <i className="fa-solid fa-star fa-xl" onClick={() => handleDeleteFavorite(post.id)}></i>
                    </div>
                )
            } else {
                return (
                    <Button color="info" onClick={() => handleAddFavorite(post.id)}>I Sitz</Button>
                )
            }
        }
    }

    const commentCheck = () => {
        if (comments.length) {
            return (
                <>
                {comments.map((comment) => (
                    <Comment comment={comment} key={comment.id}/>
                ))}
                </>
            )
        } else {
            return (
                <><p>No comments for this post yet. You can add the first!</p></>
            )
        }
    }

    useEffect(() => {
        getPost()
        getComments()
    }, [])
    
    return (
        <div style={{display: "flex", flexDirection: "column" }}>
            <Card color="light" style={{width: '40%'}}>
                <CardBody>
                    <CardTitle tag="h5">
                        {post.title}
                    </CardTitle>
                    <CardSubtitle className="mb-2 text-muted" tag="h6">
                        Posted by: {post.userProfile.name} <br/> On: {post.createdDateTime}
                    </CardSubtitle>
                </CardBody>
                    <img alt="probably a cat in a box" src={post.imageLocation} width="100%"/>
                <CardBody>
                    <CardText className="mb-2 text-muted">
                        <b>Size:</b> {post.size.name} | <b>Material:</b> {post.material.type}
                    </CardText>
                    <CardText>
                        {post.description}
                    </CardText>
                    <div>    
                        {buttonDisplay()}
                    </div>
                </CardBody>
            </Card>
            <Button color="success" size="lg" onClick={() => navigate(`/posts/${post.id}/addComment`)}>Add Comment</Button>
            {commentCheck()}
        </div>
    )
}