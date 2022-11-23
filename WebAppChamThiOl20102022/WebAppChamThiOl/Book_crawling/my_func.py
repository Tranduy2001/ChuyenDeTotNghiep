import cv2 as cv
import numpy as np
def show_images(image, title = "img", wait=True):

    cv.imshow(title, image)

    if wait:
        cv.waitKey(0)
        cv.destroyAllWindows()
        


def img_preprocessing(img):
    img = cv.cvtColor(img, cv.COLOR_BGR2GRAY)
    th, threshed = cv.threshold(img, 100, 255, cv.THRESH_BINARY_INV)
    return threshed

def get_mark_sbd(sbd_img):
    start, end = 0, 25
    cols = []
    for i in range(7):
        cols.append(sbd_img[0:240, start:end])
        start, end = start + 24, end+24
    res = []
    for c in cols:
        boxes = np.vsplit(c, 10)
        non_zero = np.array([cv.countNonZero(box) for box in boxes])
        res.append(str(np.where(non_zero == non_zero.max())[0][0]))
    return "".join(res)

def get_one_ans_pos(img):
    cols = np.hsplit(img, 5)[1:]
    non_zero = np.array([cv.countNonZero(c) for c in cols])
    if (non_zero == 0).all():
        return '0'
    pos = np.where(non_zero == non_zero.max())[0][0] + 1
    if pos == 1:
        return "A"
    if pos == 2:
        return "B"
    if pos == 3:
        return "C"
    else:
        return "D"

def get_all_ans_pos(threshed, ran = []):
    ran = [i for i in range(11)] if ran == [] else ran
    rows = []
    a, b = 0, 24
    for i in range(10):
        rows.append(threshed[a:b, :130])
        a = a + 24
        b = b + 24
    res = {}
    i = 0
    for r in rows:
        res[ran[i] + 1] = get_one_ans_pos(r)
        i += 1
    return res
    
def get_score(img):
    crop_imgs = [ img[295:530, 400:530], 
                img[550:785, 400:530], 
                img[550:785, 260:390],
                img[550:785, 120:250]
                ]
    crop_imgs = list(map(img_preprocessing, crop_imgs))

    res = {}
    start, end = 0, 11

    for crop_img in crop_imgs:
        res.update(get_all_ans_pos(crop_img, [i for i in range(start, end)]))
        start, end = start+10, end+10
    return res