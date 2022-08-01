import { Button, Card, CardBody, CardSubtitle, CardText, Modal, ModalBody, ModalFooter } from "reactstrap";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { deleteComment } from "../../modules/commentManager";
import { dateFormatter } from "../../helpers/dateFormatter";

export const Comment = ({ comment, user, commentRender, setCommentRender }) => {
    const [modal, setModal] = useState(false)

    const navigate = useNavigate()
    const toggle = () => {
        setModal(!modal)
    }    

    const handleClickDelete = () => {
        deleteComment(comment.id)
        .then(() => setCommentRender(commentRender + 1))
        .then(() => toggle())
        
    }

    return (
        <>
        <Modal isOpen={modal} toggle={toggle}>
            <ModalBody>
                <b>Are you sure you want to delete this comment?</b> <br/> "{comment.content}"
            </ModalBody>
            <ModalFooter>
                <Button color="danger" onClick={() => handleClickDelete()}>Delete</Button>{' '}
                <Button color="secondary" onClick={() => toggle()}>Cancel</Button>
            </ModalFooter>
        </Modal>
        <Card className="m-1" color="light" style={{width: '23%'}}>
            <CardBody>
                <CardSubtitle className="mb-2 text-muted" tag="h6">
                    Posted by: {comment.userProfile.name} <br/> On: {dateFormatter(comment.createdDateTime)}
                </CardSubtitle>
                <CardText>
                    {comment.content}
                </CardText>
                {comment.userProfileId === user.id ? 
                    <div className="editAndDelete">
                        <div type="button" className="bggray2 text-primary commentIcon">
                            <i className="fa-solid fa-pen-to-square" onClick={() => navigate(`/posts/editComment/${comment.id}`)}></i>
                        </div>
                        <div type="button" className="bggray2 text-danger commentIcon">
                            <i className="fa-solid fa-trash-can" onClick={() => toggle()}></i>
                        </div>
                    </div>
                    : ""
                }
            </CardBody>
        </Card>
        </>
    )
}