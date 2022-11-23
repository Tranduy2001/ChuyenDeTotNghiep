
import cv2 as cv
import json
from my_func import *

width, height = 700, 900

img = cv.imread("Img/01.png")
img = cv.resize(img, (width, height))

sbd_img = img_preprocessing(img[295:535, 130:270])
data = {}
data['data'] = {}
data['data']['sbd'] = get_mark_sbd(sbd_img)


data['data']['answer'] = get_score(img)
data = json.dumps(data)
with open('data.json', "w") as f:
    json.dump(data, f, indent = 4)
