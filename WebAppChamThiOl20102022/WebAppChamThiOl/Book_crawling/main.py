import cv2 as cv
import json
import glob

from my_func import *

width, height = 700, 900

img_files = glob.glob('Img/*.png')
list_converted_img = []
for img_file in img_files:
    img = cv.imread(img_file)
    img = cv.resize(img, (width, height))
    sbd_img = img_preprocessing(img[295:535, 130:270])
    data = {}
    data['data'] = {}
    data['data']['sbd'] = get_mark_sbd(sbd_img)


    data['data']['answer'] = get_score(img)
    list_converted_img.append(data)

data = json.dumps(list_converted_img)
jsonFile = open("data.json", "w")
jsonFile.write(data)
jsonFile.close()
print(list_converted_img)