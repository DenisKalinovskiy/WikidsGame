﻿using UnityEngine;

// Пространство имен для ядра игры
namespace RPG.Core
{
    // Класс для управления камерой, следующей за целью
    public class FollowCamera : MonoBehaviour
    {
        private Transform target; // Цель, за которой следует камера
        [SerializeField] private float rotationSpeed; // Скорость вращения камеры
        [SerializeField] private float zoomSpeed; // Скорость приближения/удаления камеры
        public bool trackPlayer = true; // Флаг для отслеживания игрока

        // Ссылки на камеру и начальное положение камеры
        private Transform defaultCameraTransform;
        private Camera mainCam;

        // Флаги для определения возможности двигаться/масштабироваться/вращаться камере
        private bool canRotate;
        private bool canZoom;

        // Переменные для вращения камеры
        private float camXRotation = 0;
        private float camYRotation = 0;

        // Пределы масштабирования
        [SerializeField] private float minZoom;
        [SerializeField] private float maxZoom;
        private float zoomTotal; // Общее изменение масштаба
        private float zoomAmt; // Количество изменения масштаба

        // Метод вызывается перед первым обновлением кадра
        void Start()
        {
            mainCam = Camera.main; // Получаем главную камеру
            defaultCameraTransform = transform; // Сохраняем начальное положение камеры
            canRotate = true; // Устанавливаем возможность вращения
            canZoom = true; // Устанавливаем возможность масштабирования
            target = MainPlayer.Instance.transform; // Получаем цель (обычно игрока)
        }

        // Метод вызывается один раз за кадр, после Update
        void LateUpdate()
        {
            cameraMovement(); // Вызываем метод для управления камерой
        }

        // Метод для управления камерой
        private void cameraMovement()
        {
            // Переключаем отслеживание игрока при нажатии клавиши LeftControl
            if (Input.GetKeyDown(KeyCode.LeftControl))
                trackPlayer = !trackPlayer;

            if (trackPlayer)
                transform.position = target.position; // Позиционируем камеру на цели

            // Обрабатываем вращение камеры при нажатии правой кнопки мыши
            if (canRotate && Input.GetMouseButton(1))
            {
                RotationMovement(); // Вызываем метод для вращения
            }

            if (canZoom)
            {
                zoomMovement(); // Вызываем метод для масштабирования
            }
        }

        // Метод для вращения камеры
        private void RotationMovement()
        {
            // Получаем значения вращения по осям X и Y
            camYRotation += (Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime);
            camXRotation += (Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime);

            // Ограничиваем вращение по оси X
            camXRotation = Mathf.Clamp(camXRotation, -25, 30);
            // Применяем вращение к камере
            transform.localEulerAngles = new Vector3(camXRotation, camYRotation, 0);
        }

        // Метод для масштабирования камеры
        private void zoomMovement()
        {
            // Получаем количество изменения масштаба
            zoomAmt = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
            // Прибавляем это изменение к общему изменению масштаба
            zoomTotal += zoomAmt;
            // Ограничиваем общее изменение масштаба
            zoomTotal = Mathf.Clamp(zoomTotal, minZoom, maxZoom);

            // Получаем новую позицию для масштабирования
            Vector3 newZoomPos = mainCam.transform.position + (mainCam.transform.forward * zoomAmt);

            // Масштабируем камеру, если она находится в пределах допустимого масштабирования
            if (zoomTotal > minZoom && zoomTotal < maxZoom)
                mainCam.transform.position = newZoomPos;
        }
    }
}